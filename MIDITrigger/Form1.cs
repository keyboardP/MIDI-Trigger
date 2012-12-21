///MIDI Trigger by keyboardP (www.keyboardp.me) 
///This is a basic proof of concept to show MIDI input devices being used to trigger events.
///It is *not* a secure way of handling passwords and was created just for fun
///and to demonstrate basic MIDI input/output handling using NAudio.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.Midi;

namespace MIDITrigger
{
    public partial class Form1 : Form
    {
        //hold the midi input device
        MidiIn mIn;
        //used to hear the sound (i.e. midi output device)
        MidiOut mOut;
        
        
        //this will hold the actual password stored. When the password matches the inputted keys, some event is triggered (Messagebox is displayed in this example).
        List<string> passwordNotes = new List<string>();
        //this will hold the notes that have been played (if the 'Clear' button is pressed, this list is cleared)
        List<string> currentNotes = new List<string>();
        
        //flag used to determine if the user is recording (i.e. setting the password) or just playing the notes        
        bool isRecording = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //iterate through all MidiIn devices and add them to the listbox so the user can choose which one to use
            for (int i = 0; i < MidiIn.NumberOfDevices; i++)
            {
                lbInputDevices.Items.Add(MidiIn.DeviceInfo(i).ProductName);
                
                //regardless of how many devices are added (assuming at least one), the default will be the first one in the list
                lbInputDevices.SelectedIndex = 0;
            }

            //iterate through all MidiOut devices and add them to the listbox so the user can choose which one to use
            for (int i = 0; i < MidiOut.NumberOfDevices; i++)
            {
                lbOutputDevices.Items.Add(MidiOut.DeviceInfo(i).ProductName);

                //regardless of how many devices are added (assuming at least one), the default will be the first one in the list
                lbOutputDevices.SelectedIndex = 0;
            }

         
            if (mIn == null)
            {
                if (lbInputDevices.Items.Count > 0)
                {
                    //create the new MidiIn object and set up its event handlers
                    mIn = new MidiIn(lbInputDevices.SelectedIndex);
                    mIn.MessageReceived += new EventHandler<MidiInMessageEventArgs>(midiIn_MessageReceived);
                    mIn.ErrorReceived += (s, o) => { MessageBox.Show("Whoops, an error occurred!"); };

                    //start listening
                    mIn.Start();

                    //create out midi out (so that we can hear the sound playing)
                    if (mOut == null)
                    {
                        mOut = new MidiOut(lbOutputDevices.SelectedIndex);
                    }
                }
                else
                {
                    //warn the user that no midi devices have been found and that they should connect one before clicking Retry
                    if (MessageBox.Show("Please connect a MIDI device and click Retry otherwise click Cancel to exit.", "No MIDI Devices Found", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    {
                        //restart the program if the user selected 'Retry'
                        System.Diagnostics.Process.Start(Application.ExecutablePath);
                        Application.Exit();
                    }
                    else
                        Application.Exit();
                }
            }

           
            
        }

        /// <summary>
        /// When an input message is received, this event fires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            //cast e.MidiEvent as a NoteEvent so that we can access the relevant properties directly
            NoteEvent noteEvt = e.MidiEvent as NoteEvent;

            //if the casting was successful
            if (noteEvt != null)
            {
                
                //send the key the user pressed to the MidiOut device to hear the note
                mOut.Send(e.MidiEvent.GetAsShortMessage());              


                //only listen to the noteon commands to avoid duplicates            
                if (NoteEvent.IsNoteOn(e.MidiEvent))
                {
                    //Sometimes a Cross Thread exception is thrown which means that we're trying to update the UI from a non-UI thread.
                    //To avoid this, we check if we need to marshall data to the UI thread at this point in time.
                    //If so, we send the data from the background thread to the UI thread and then update the UI controls
                    if (this.InvokeRequired)
                    {
                        this.Invoke((Action)(() =>
                            {
                                tbPlayed.Text += noteEvt.NoteName + "\r\n";

                                //scroll to the bottom of the textbox when the user reaches the bottom
                                tbPlayed.SelectionStart = tbPlayed.Text.Length;
                                tbPlayed.ScrollToCaret();
                            }
                        ));
                    }
                    else
                    {
                        //no marshalling required and we can update the UI controls directly

                        tbPlayed.Text += noteEvt.NoteName + "\r\n";

                        //scroll to the bottom of the textbox when the user reaches the bottom
                        tbPlayed.SelectionStart = tbPlayed.Text.Length;
                        tbPlayed.ScrollToCaret();
                    }

                     
                       
                  
                    //if it's recording, add to password list
                    if (isRecording)
                    {
                        passwordNotes.Add(noteEvt.NoteName);
                    }
                    else
                    {
                        //user is not recording. At this point, we listen to see if the user has entered the password
                        
                        //add the note just played
                        currentNotes.Add(noteEvt.NoteName);

                        //check if the current sequence played matches the password sequence
                        if (passwordNotes.SequenceEqual(currentNotes))                      
                        {
                            //sequences matches, event triggered (e.g. could unlock a door in a game)
                            MessageBox.Show("Correct keys played!");                    
                            Clear();
                        }
                    }                   
                }
              
            }

        }

        /// <summary>
        /// Detect whether the user is recording or wanting to record. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecordSave_Click(object sender, EventArgs e)
        {
            //if the user is recording, when this button is pressed, it should stop recording
            //and clear any notes entered.
            if (isRecording)
            {
                //disable recording
                isRecording = false;

                //clear textbox and current notes entered
                Clear();

                //is recording
                btnRecordSave.Text = "Record Password";

            }
            else
            {
                //user has started recording so set the basic details and clear the current password

                isRecording = true;

                //not recording
                btnRecordSave.Text = "Save Password";

                //clear current password
                passwordNotes.Clear();

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //clear the textbox and any notes played so far (password is unaffected)
            Clear();
        }

        private void Clear()
        {
            //clear the textbox (either by marshalling from background thread to UI or directly on the UI)
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() =>  {
                        //clear the textbox 
                        tbPlayed.Clear();
                    }));
            }
            else
                tbPlayed.Clear();

            //clear the current notes
            currentNotes.Clear();
        }

        private void lbInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the user chooses another input, create a new MIDI input device and use that
            if (mIn != null)
            {
                //dispose the current midi in and create a new one
                mIn.Dispose();                

                //create a new MIDI input object with the newly selected input device
                mIn = new MidiIn(lbInputDevices.SelectedIndex);                
                mIn.MessageReceived += new EventHandler<MidiInMessageEventArgs>(midiIn_MessageReceived);
                mIn.ErrorReceived += (s, o) => { MessageBox.Show("Whoops, an error occurred!"); };

                mIn.Start();
            }
        }

        private void lbOutputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the user chooses another output, use a new MIDI output device and use that
            if (mOut != null)
            {
                //dispose the current midi in and create a new one
                mOut.Dispose();

                //create a new MIDI input object with the newly selected input device
                mOut = new MidiOut(lbOutputDevices.SelectedIndex);
               
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //dispose the midi inputs and outputs
            if (mIn != null)
            {
                mIn.Dispose();
                mIn = null;
            }

            if (mOut != null)
            {
                mOut.Dispose();
                mOut = null;
            }
        }

        
    }
}

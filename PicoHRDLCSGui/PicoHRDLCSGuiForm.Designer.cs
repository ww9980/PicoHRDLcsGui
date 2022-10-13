/******************************************************************************
 *
 * Filename: PicoHRDLCSGuiForm.Designer.cs
 * 
 * Copyright © 2016-2017 Pico Technology Ltd. See LICENSE file for terms.
 *
 ******************************************************************************/

namespace PicoHRDLGui
{
    partial class PicoHRDLCSGuiForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.runButton = new System.Windows.Forms.Button();
            this.unitInfoTextBox = new System.Windows.Forms.TextBox();
            this.channel1DataTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.numValuesCollectedLabel = new System.Windows.Forms.Label();
            this.numSamplesCollectedTextBox = new System.Windows.Forms.TextBox();
            this.openButton = new System.Windows.Forms.Button();
            this.numSamplesPerChannelTextBox = new System.Windows.Forms.TextBox();
            this.numValuesToCollectLabel = new System.Windows.Forms.Label();
            this.channel1DataLabel = new System.Windows.Forms.Label();
            this.unitInformationLabel = new System.Windows.Forms.Label();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.btnSS = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.timerPlot = new System.Windows.Forms.Timer(this.components);
            this.btnLiveStop = new System.Windows.Forms.Button();
            this.timerUpdatePlot = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(202, 12);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // unitInfoTextBox
            // 
            this.unitInfoTextBox.Location = new System.Drawing.Point(21, 79);
            this.unitInfoTextBox.Multiline = true;
            this.unitInfoTextBox.Name = "unitInfoTextBox";
            this.unitInfoTextBox.Size = new System.Drawing.Size(164, 178);
            this.unitInfoTextBox.TabIndex = 1;
            // 
            // channel1DataTextBox
            // 
            this.channel1DataTextBox.Location = new System.Drawing.Point(202, 79);
            this.channel1DataTextBox.Multiline = true;
            this.channel1DataTextBox.Name = "channel1DataTextBox";
            this.channel1DataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.channel1DataTextBox.Size = new System.Drawing.Size(283, 394);
            this.channel1DataTextBox.TabIndex = 2;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(102, 12);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // numValuesCollectedLabel
            // 
            this.numValuesCollectedLabel.AutoSize = true;
            this.numValuesCollectedLabel.Location = new System.Drawing.Point(18, 344);
            this.numValuesCollectedLabel.Name = "numValuesCollectedLabel";
            this.numValuesCollectedLabel.Size = new System.Drawing.Size(122, 13);
            this.numValuesCollectedLabel.TabIndex = 4;
            this.numValuesCollectedLabel.Text = "Num Samples Collected:";
            // 
            // numSamplesCollectedTextBox
            // 
            this.numSamplesCollectedTextBox.Location = new System.Drawing.Point(21, 360);
            this.numSamplesCollectedTextBox.Name = "numSamplesCollectedTextBox";
            this.numSamplesCollectedTextBox.ReadOnly = true;
            this.numSamplesCollectedTextBox.Size = new System.Drawing.Size(100, 20);
            this.numSamplesCollectedTextBox.TabIndex = 5;
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(21, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 6;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // numSamplesPerChannelTextBox
            // 
            this.numSamplesPerChannelTextBox.Location = new System.Drawing.Point(21, 298);
            this.numSamplesPerChannelTextBox.Name = "numSamplesPerChannelTextBox";
            this.numSamplesPerChannelTextBox.Size = new System.Drawing.Size(100, 20);
            this.numSamplesPerChannelTextBox.TabIndex = 7;
            this.numSamplesPerChannelTextBox.Text = "10";
            // 
            // numValuesToCollectLabel
            // 
            this.numValuesToCollectLabel.AutoSize = true;
            this.numValuesToCollectLabel.Location = new System.Drawing.Point(18, 282);
            this.numValuesToCollectLabel.Name = "numValuesToCollectLabel";
            this.numValuesToCollectLabel.Size = new System.Drawing.Size(125, 13);
            this.numValuesToCollectLabel.TabIndex = 8;
            this.numValuesToCollectLabel.Text = "Num Samples / Channel:";
            // 
            // channel1DataLabel
            // 
            this.channel1DataLabel.AutoSize = true;
            this.channel1DataLabel.Location = new System.Drawing.Point(199, 63);
            this.channel1DataLabel.Name = "channel1DataLabel";
            this.channel1DataLabel.Size = new System.Drawing.Size(84, 13);
            this.channel1DataLabel.TabIndex = 9;
            this.channel1DataLabel.Text = "Channel 1 Data:";
            // 
            // unitInformationLabel
            // 
            this.unitInformationLabel.AutoSize = true;
            this.unitInformationLabel.Location = new System.Drawing.Point(21, 62);
            this.unitInformationLabel.Name = "unitInformationLabel";
            this.unitInformationLabel.Size = new System.Drawing.Size(84, 13);
            this.unitInformationLabel.TabIndex = 10;
            this.unitInformationLabel.Text = "Unit Information:";
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(491, 80);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(675, 393);
            this.formsPlot1.TabIndex = 11;
            // 
            // btnSS
            // 
            this.btnSS.Location = new System.Drawing.Point(291, 12);
            this.btnSS.Name = "btnSS";
            this.btnSS.Size = new System.Drawing.Size(90, 22);
            this.btnSS.TabIndex = 12;
            this.btnSS.Text = "Start/stop";
            this.btnSS.UseVisualStyleBackColor = true;
            this.btnSS.Click += new System.EventHandler(this.btnSS_Click);
            // 
            // btnLive
            // 
            this.btnLive.Location = new System.Drawing.Point(387, 11);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(90, 22);
            this.btnLive.TabIndex = 13;
            this.btnLive.Text = "Live";
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // timerPlot
            // 
            this.timerPlot.Interval = 1000;
            this.timerPlot.Tick += new System.EventHandler(this.timerPlot_Tick);
            // 
            // btnLiveStop
            // 
            this.btnLiveStop.Location = new System.Drawing.Point(483, 11);
            this.btnLiveStop.Name = "btnLiveStop";
            this.btnLiveStop.Size = new System.Drawing.Size(90, 22);
            this.btnLiveStop.TabIndex = 14;
            this.btnLiveStop.Text = "LiveStop";
            this.btnLiveStop.UseVisualStyleBackColor = true;
            this.btnLiveStop.Click += new System.EventHandler(this.btnLiveStop_Click);
            // 
            // timerUpdatePlot
            // 
            this.timerUpdatePlot.Interval = 750;
            this.timerUpdatePlot.Tick += new System.EventHandler(this.timerUpdatePlot_Tick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(607, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 22);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "SaveData";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(703, 11);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 22);
            this.btnOpen.TabIndex = 16;
            this.btnOpen.Text = "OpenData";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // PicoHRDLCSGuiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 496);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLiveStop);
            this.Controls.Add(this.btnLive);
            this.Controls.Add(this.btnSS);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.unitInformationLabel);
            this.Controls.Add(this.channel1DataLabel);
            this.Controls.Add(this.numValuesToCollectLabel);
            this.Controls.Add(this.numSamplesPerChannelTextBox);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.numSamplesCollectedTextBox);
            this.Controls.Add(this.numValuesCollectedLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.channel1DataTextBox);
            this.Controls.Add(this.unitInfoTextBox);
            this.Controls.Add(this.runButton);
            this.Name = "PicoHRDLCSGuiForm";
            this.Text = "PicoLog HRDL (picohrdl) C# GUI Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox unitInfoTextBox;
        private System.Windows.Forms.TextBox channel1DataTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label numValuesCollectedLabel;
        private System.Windows.Forms.TextBox numSamplesCollectedTextBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.TextBox numSamplesPerChannelTextBox;
        private System.Windows.Forms.Label numValuesToCollectLabel;
        private System.Windows.Forms.Label channel1DataLabel;
        private System.Windows.Forms.Label unitInformationLabel;
        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.Button btnSS;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.Timer timerPlot;
        private System.Windows.Forms.Button btnLiveStop;
        private System.Windows.Forms.Timer timerUpdatePlot;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpen;
    }
}


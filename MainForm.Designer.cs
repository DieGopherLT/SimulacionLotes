namespace SimulacionLotes
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            ProcessesInput = new TextBox();
            StartButton = new Button();
            onWaitRTB = new RichTextBox();
            PendingBatchesLabel = new Label();
            OnWaitLabel = new Label();
            OnExecutionRTB = new RichTextBox();
            OnFinishedRTB = new RichTextBox();
            OnExecutionLabel = new Label();
            OnFinishedLabel = new Label();
            GenerateResultsButton = new Button();
            CronometerLabel = new Label();
            SecondsPassedLabel = new Label();
            CancellationButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(28, 37);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 0;
            label1.Text = "# Procesos";
            // 
            // ProcessesInput
            // 
            ProcessesInput.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ProcessesInput.Location = new Point(126, 35);
            ProcessesInput.Name = "ProcessesInput";
            ProcessesInput.Size = new Size(92, 27);
            ProcessesInput.TabIndex = 1;
            // 
            // StartButton
            // 
            StartButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            StartButton.Location = new Point(28, 68);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(190, 29);
            StartButton.TabIndex = 2;
            StartButton.Text = "Generar";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // onWaitRTB
            // 
            onWaitRTB.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            onWaitRTB.Location = new Point(28, 137);
            onWaitRTB.Name = "onWaitRTB";
            onWaitRTB.Size = new Size(248, 321);
            onWaitRTB.TabIndex = 3;
            onWaitRTB.Text = "";
            // 
            // PendingBatchesLabel
            // 
            PendingBatchesLabel.AutoSize = true;
            PendingBatchesLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            PendingBatchesLabel.Location = new Point(28, 474);
            PendingBatchesLabel.Name = "PendingBatchesLabel";
            PendingBatchesLabel.Size = new Size(137, 20);
            PendingBatchesLabel.TabIndex = 4;
            PendingBatchesLabel.Text = "# Lotes pendientes:";
            // 
            // OnWaitLabel
            // 
            OnWaitLabel.AutoSize = true;
            OnWaitLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            OnWaitLabel.Location = new Point(109, 114);
            OnWaitLabel.Name = "OnWaitLabel";
            OnWaitLabel.Size = new Size(73, 20);
            OnWaitLabel.TabIndex = 5;
            OnWaitLabel.Text = "En espera";
            // 
            // OnExecutionRTB
            // 
            OnExecutionRTB.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            OnExecutionRTB.Location = new Point(305, 202);
            OnExecutionRTB.Name = "OnExecutionRTB";
            OnExecutionRTB.Size = new Size(186, 256);
            OnExecutionRTB.TabIndex = 6;
            OnExecutionRTB.Text = "";
            // 
            // OnFinishedRTB
            // 
            OnFinishedRTB.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            OnFinishedRTB.Location = new Point(521, 137);
            OnFinishedRTB.Name = "OnFinishedRTB";
            OnFinishedRTB.Size = new Size(248, 321);
            OnFinishedRTB.TabIndex = 7;
            OnFinishedRTB.Text = "";
            // 
            // OnExecutionLabel
            // 
            OnExecutionLabel.AutoSize = true;
            OnExecutionLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            OnExecutionLabel.Location = new Point(351, 179);
            OnExecutionLabel.Name = "OnExecutionLabel";
            OnExecutionLabel.Size = new Size(92, 20);
            OnExecutionLabel.TabIndex = 8;
            OnExecutionLabel.Text = "En ejecución";
            // 
            // OnFinishedLabel
            // 
            OnFinishedLabel.AutoSize = true;
            OnFinishedLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            OnFinishedLabel.Location = new Point(604, 114);
            OnFinishedLabel.Name = "OnFinishedLabel";
            OnFinishedLabel.Size = new Size(86, 20);
            OnFinishedLabel.TabIndex = 9;
            OnFinishedLabel.Text = "Terminados";
            // 
            // GenerateResultsButton
            // 
            GenerateResultsButton.Enabled = false;
            GenerateResultsButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            GenerateResultsButton.Location = new Point(614, 486);
            GenerateResultsButton.Name = "GenerateResultsButton";
            GenerateResultsButton.Size = new Size(155, 33);
            GenerateResultsButton.TabIndex = 10;
            GenerateResultsButton.Text = "Obtener resultados";
            GenerateResultsButton.UseVisualStyleBackColor = true;
            GenerateResultsButton.Click += GenerateResultsButton_Click;
            // 
            // CronometerLabel
            // 
            CronometerLabel.AutoSize = true;
            CronometerLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            CronometerLabel.Location = new Point(521, 42);
            CronometerLabel.Name = "CronometerLabel";
            CronometerLabel.Size = new Size(93, 20);
            CronometerLabel.TabIndex = 11;
            CronometerLabel.Text = "Reloj global:";
            // 
            // SecondsPassedLabel
            // 
            SecondsPassedLabel.AutoSize = true;
            SecondsPassedLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            SecondsPassedLabel.Location = new Point(620, 42);
            SecondsPassedLabel.Name = "SecondsPassedLabel";
            SecondsPassedLabel.Size = new Size(17, 20);
            SecondsPassedLabel.TabIndex = 13;
            SecondsPassedLabel.Text = "0";
            // 
            // CancellationButton
            // 
            CancellationButton.Enabled = false;
            CancellationButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            CancellationButton.Location = new Point(323, 468);
            CancellationButton.Name = "CancellationButton";
            CancellationButton.Size = new Size(155, 33);
            CancellationButton.TabIndex = 14;
            CancellationButton.Text = "Cancelar proceso";
            CancellationButton.UseVisualStyleBackColor = true;
            CancellationButton.Click += CancelationButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(816, 550);
            Controls.Add(CancellationButton);
            Controls.Add(SecondsPassedLabel);
            Controls.Add(CronometerLabel);
            Controls.Add(GenerateResultsButton);
            Controls.Add(OnFinishedLabel);
            Controls.Add(OnExecutionLabel);
            Controls.Add(OnFinishedRTB);
            Controls.Add(OnExecutionRTB);
            Controls.Add(OnWaitLabel);
            Controls.Add(PendingBatchesLabel);
            Controls.Add(onWaitRTB);
            Controls.Add(StartButton);
            Controls.Add(ProcessesInput);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "Simulación lotes";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox ProcessesInput;
        private Button StartButton;
        private RichTextBox onWaitRTB;
        private Label PendingBatchesLabel;
        private Label OnWaitLabel;
        private RichTextBox OnExecutionRTB;
        private RichTextBox OnFinishedRTB;
        private Label OnExecutionLabel;
        private Label OnFinishedLabel;
        private Button GenerateResultsButton;
        private Label CronometerLabel;
        private Label SecondsPassedLabel;
        private Button CancellationButton;
    }
}
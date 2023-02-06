namespace TarkovItemCostOverlay
{
    partial class ItemCostOverlay
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
            this.captureTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // captureTimer
            // 
            this.captureTimer.Enabled = true;
            this.captureTimer.Interval = 1000;
            this.captureTimer.Tick += new System.EventHandler(this.captureTimer_Tick);
            // 
            // ItemCostOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ItemCostOverlay";
            this.Text = "ItemCostOverlay";
            this.Load += new System.EventHandler(this.ItemCostOverlay_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ItemCostOverlay_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer captureTimer;
    }
}
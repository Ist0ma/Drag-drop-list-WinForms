namespace Drag_drop_list_WinForms
{
    partial class DragAndSort
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new Size(200, 200);
            this.Text = "DragAndSort";

            addButton = new Button();
            addButton.Text = "Добавить PictureBox";
            addButton.Location = new Point(10, 10);
            addButton.Click += new EventHandler(AddPictureBox_Click);
            Controls.Add(addButton);


            highlightPictureBox = new PictureBox();
            highlightPictureBox.Size = new Size(150, 50);
            highlightPictureBox.BackColor = Color.FromArgb(128, Color.DodgerBlue);
            highlightPictureBox.BorderStyle = BorderStyle.FixedSingle;
            highlightPictureBox.Hide();
            Controls.Add(highlightPictureBox);
            Controls.SetChildIndex(highlightPictureBox, 0);
        }

        #endregion

        private Button addButton;
        private PictureBox highlightPictureBox;
    }
}
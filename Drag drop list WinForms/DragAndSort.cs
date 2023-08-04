using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drag_drop_list_WinForms
{
    public partial class DragAndSort : Form
    {
        const int ElemHeight = 50;
        const int SpaceBetween = 10;


        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private PictureBox selectedPictureBox;
        private int selectedPictureBoxIndex;
        private Point mouseDownLocation;

        public DragAndSort()
        {
            InitializeComponent();
        }

        private void CreatePictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(150, ElemHeight);
            pictureBox.Location = new Point(10, ElemHeight + (pictureBoxes.Count) * (ElemHeight + SpaceBetween));
            pictureBox.BackColor = Color.LightGray;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Paint += PictureBox_Paint;
            pictureBox.Name = pictureBoxes.Count.ToString();

            pictureBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);
            pictureBox.MouseMove += new MouseEventHandler(PictureBox_MouseMove);
            pictureBox.MouseUp += new MouseEventHandler(PictureBox_MouseUp);

            pictureBoxes.Add(pictureBox);
            Controls.Add(pictureBox);

            if (pictureBox.Top + pictureBox.Height + ElemHeight >= Height)
            {
                Height = pictureBox.Top + pictureBox.Height + ElemHeight;
            }
        }
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            int numberToDraw = int.Parse(pictureBox.Name);

            Font font = new Font("Arial", 16);
            Brush brush = new SolidBrush(Color.Black);

            SizeF textSize = e.Graphics.MeasureString(numberToDraw.ToString(), font);
            PointF textPosition = new PointF((pictureBox.Width - textSize.Width) / 2, (pictureBox.Height - textSize.Height) / 2);
            e.Graphics.DrawString(numberToDraw.ToString(), font, brush, textPosition);
        }

        private void AddPictureBox_Click(object sender, EventArgs e)
        {
            CreatePictureBox();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            selectedPictureBox = (PictureBox)sender;
            selectedPictureBoxIndex = pictureBoxes.IndexOf(selectedPictureBox);
            mouseDownLocation = e.Location;
            selectedPictureBox.BringToFront();

            highlightPictureBox.Show();
            highlightPictureBox.Location = selectedPictureBox.Location;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedPictureBox == null || e.Button != MouseButtons.Left)
                return;

            int newY = selectedPictureBox.Top + e.Y - mouseDownLocation.Y;
            int pictureBoxHeight = selectedPictureBox.Height;

            if (newY < ElemHeight)
                newY = ElemHeight;
            else if (newY + pictureBoxHeight > ClientSize.Height)
                newY = ClientSize.Height - pictureBoxHeight;

            selectedPictureBox.Top = newY;

            int newIndex = (selectedPictureBox.Top - ElemHeight + e.Y) / (ElemHeight + SpaceBetween);
            if (newIndex < 0)
                newIndex = 0;
            else if (newIndex >= pictureBoxes.Count)
                newIndex = pictureBoxes.Count - 1;

            if (newIndex != selectedPictureBoxIndex)
            {
                if (selectedPictureBoxIndex < newIndex)
                {
                    for (int i = selectedPictureBoxIndex + 1; i <= newIndex; i++)
                    {
                        pictureBoxes[i].Top = ElemHeight + (i - 1) * (ElemHeight + SpaceBetween);
                    }
                }
                else
                {
                    for (int i = selectedPictureBoxIndex - 1; i >= newIndex; i--)
                    {
                        pictureBoxes[i].Top = ElemHeight + (i + 1) * (ElemHeight + SpaceBetween);
                    }
                }

                highlightPictureBox.Top = ElemHeight + newIndex * (ElemHeight + SpaceBetween);

                pictureBoxes.RemoveAt(selectedPictureBoxIndex);
                pictureBoxes.Insert(newIndex, selectedPictureBox);
                selectedPictureBoxIndex = newIndex;
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            selectedPictureBox.Location = highlightPictureBox.Location;

            selectedPictureBox = null;
            highlightPictureBox.Hide();
        }
    }
}

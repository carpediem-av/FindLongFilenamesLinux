/**********************************************************************/
/* Copyright (c) 2024 Carpe Diem Software Developing by Alex Versetty */
/* http://carpediem.0fees.us                                          */
/**********************************************************************/

using System.Windows.Forms;
using System.Drawing;

namespace CDSD.Forms
{
	public partial class FAbout : Form
	{
		public FAbout()
		{
			InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

		private void www_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://carpediem.0fees.us");
		}
	}
}

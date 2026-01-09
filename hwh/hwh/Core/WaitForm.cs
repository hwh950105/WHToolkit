using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hwh.Core
{
    public class WaitForm : Form
    {
        private readonly Label _label;
        private readonly ProgressBar _bar;

        public WaitForm(string message = "대기중...")
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            TopMost = true;

            Width = 260;
            Height = 120;

            _label = new Label
            {
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = message
            };

            _bar = new ProgressBar
            {
                Dock = DockStyle.Top,
                Height = 18,
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30
            };

            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
            panel.Controls.Add(_bar);
            panel.Controls.Add(_label);

            Controls.Add(panel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DieuKhienNgoiNhaThongMinh
{
    public partial class Load : Form
    {
        public Load(String title)
        {
            InitializeComponent();
            label1.Text = title;
        }
    }
}

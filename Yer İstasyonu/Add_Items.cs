using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yer_İstasyonu
{
    class Add_Items : ProgressBar
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams ct = base.CreateParams;
                ct.Style |= 0X04;

                return ct;
            }
        }
    }
}

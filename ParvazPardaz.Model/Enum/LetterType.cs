using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum LetterType
    {
        //استعلام
        [Description("استعلام")]
        Inquery = 1,
        //مناقصه
        [Description("مناقصه")]
        Tender = 2,
        //مزایده
        [Description("مزایده")]
        Auction = 3
    }
}

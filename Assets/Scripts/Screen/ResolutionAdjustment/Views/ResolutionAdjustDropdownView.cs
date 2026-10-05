using Ovoshnig.UI.Basic;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Ovoshnig.Screen.ResolutionAdjustment
{
    public class ResolutionAdjustDropdownView : DropdownView
    {
        public void SetOptions(IReadOnlyList<ResolutionData> resolutions)
        {
            List<TMP_Dropdown.OptionData> options = resolutions
                .Select(r => new TMP_Dropdown.OptionData(r.ToString()))
                .ToList();

            SetOptions(options);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using TMPro;

public class ResolutionAdjusterView : DropdownView
{
    public void SetOptions(IReadOnlyList<ResolutionData> resolutions)
    {
        List<TMP_Dropdown.OptionData> options = resolutions
            .Select(r => new TMP_Dropdown.OptionData(r.ToString()))
            .ToList();

        SetOptions(options);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FormsMvvm2019
{
    public class MainViewModel : BaseViewModel
    {
		public string PassCodePlaceholder => "PASS CODE";

		private string _PassCode;

		public string PassCode
		{
			get { return _PassCode; }
			set { _PassCode = value; OnPropertyChanged(); }
		}

		public string ButtonCommitText => Properties.Resources.MainButtonCommitText;
	}
}

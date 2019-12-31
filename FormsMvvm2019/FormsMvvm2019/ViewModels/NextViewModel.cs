using System;
using System.Collections.Generic;
using System.Text;

namespace FormsMvvm2019
{
    public class NextViewModel : BaseViewModel
    {
		private string _Code;

		public string Code
		{
			get { return _Code; }
			set { _Code = value; OnPropertyChanged(); }
		}

	}
}

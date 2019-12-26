using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FormsMvvm2019
{
    public class MainViewModel : BaseViewModel
    {
		public MainViewModel()
		{
			PassCode = null;
		}

		private string _PassCode;
		[Display(Name = "Name", ResourceType = typeof(Properties.PassCode))]
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Properties.ErrorMessage))]
		public string PassCode
		{
			get { return _PassCode; }
			set { _PassCode = value; OnPropertyChanged(); PassCodeError = PropertyValidation(value); }
		}

		private string _PassCodeError;

		public string PassCodeError
		{
			get { return _PassCodeError; }
			set { _PassCodeError = value; OnPropertyChanged(); }
		}

		public string ButtonCommitText => Properties.Resources.MainButtonCommitText;
	}
}

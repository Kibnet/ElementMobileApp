using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using System.Threading;
using System.Threading.Tasks;
namespace Element
{
	public class ElementViewModel:ReactiveObject
	{
		public ElementViewModel()
		{
			SignIn = ReactiveCommand.CreateFromTask(async () =>
			{
				try
				{
					IsBusy = true;
					await Task.Delay(1000);
					token = "token";
					Status = $"Sign in by {Login}";
				}
				catch(Exception e)
				{
					
				}
				finally
				{
					IsBusy = false;
				}
			}, this.WhenAny(m=>m.Login, m=>m.Password, m=>m.token, (log, pas, tok) => log.Value!=null && pas.Value!=null && tok.Value==null));
			SignOut = ReactiveCommand.CreateFromTask(async () =>
			{
				token = null;
				Status = $"Sign out by {Login}";
			}, this.WhenAny(m => m.token, (tok) => tok.Value != null));
			this.ObservableForProperty(m=>m.token).Subscribe((obj) => 
			{
				if(token!=null)
				{
					Password = null;
					ShowLogin = false;
					ShowControl = true;
				}
				else
				{
					ShowControl = false;
					ShowLogin = true;
				}
			});
			Lock = ReactiveCommand.Create(() => Status = "Locked");
			Unlock = ReactiveCommand.Create(() => Status = "Unlocked");
		}

		[Reactive]
		public string Status { get; set;}

		public ReactiveCommand Lock { get; set; }

		public ReactiveCommand Unlock { get; set; }


		public bool? GuardMode { get; set; }

		public bool? ServiceMode { get; set; }

		[Reactive]
		public bool IsBusy { get; set; } = false;


		[Reactive]
		private string token { get; set; }

		public ReactiveCommand SignIn { get; set; }

		public ReactiveCommand SignOut { get; set; }

		[Reactive]
		public string Login { get; set; }

		[Reactive]
		public string Password { get; set; }

		[Reactive]
		public bool ShowLogin { get; set; } = true;

		[Reactive]
		public bool ShowControl { get; set; }

	}
}

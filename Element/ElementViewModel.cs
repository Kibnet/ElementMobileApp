using System;
using ReactiveUI;
using Xamarin.Forms;
using System.Threading.Tasks;
namespace Element
{
	public class ElementViewModel : ReactiveObject
	{
		public ElementViewModel()
		{
			SignIn = ReactiveCommand.CreateFromTask(async () =>
			{
				try
				{
					IsBusy = true;
					//TODO Login from REST
					await Task.Delay(2000);
					Token = "token";
					Status = $"Sign in by {Login}";
				}
				catch (Exception e)
				{
					Status = e.Message;
				}
				finally
				{
					IsBusy = false;
				}
			}, this.WhenAny(m => m.Login, m => m.Password, m => m.Token, 
			                (log, pas, tok) => string.IsNullOrWhiteSpace(log.Value) == false && 
			                string.IsNullOrWhiteSpace(pas.Value) == false && 
			                tok.Value == null));

			SignOut = ReactiveCommand.CreateFromTask(async () =>
			{
				//TODO Confirm from user
				var confirm = await Application.Current.MainPage.DisplayActionSheet("Are you sure you want log out?", null, null, new string[] { "Yes", "No" });
				if (confirm == "Yes")
				{
					await Task.Delay(1000);
					Token = null;
					Status = $"Sign out by {Login}";
				}
			}, this.WhenAny(m => m.Token, (tok) => tok.Value != null));

			this.ObservableForProperty(m => m.Token).Subscribe((obj) =>
			  {
				  if (Token != null)
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

			this.ObservableForProperty(m => m.GuardMode).Subscribe(async (obj) =>
			  {
				  IsBusy = true;
				  CanGuard = false;
				  //TODO REST
				  await Task.Delay(2000);
				  var result = obj.Value;
				  Status = $"Guard is {(result ? "On" : "Off")}";
				  CanGuard = true;
				  IsBusy = false;
			  });

			this.ObservableForProperty(m => m.ServiceMode).Subscribe(async (obj) =>
			  {
				  IsBusy = true;
				  CanService = false;
				  //TODO REST
				  await Task.Delay(2000);
				  var result = obj.Value;
				  Status = $"Service mode is {(result ? "On" : "Off")}";
				  CanService = true;
				  IsBusy = false;
			  });

			Lock = ReactiveCommand.Create(async () =>
			{
				IsBusy = true;
				CanLock = false;
				//TODO REST
				await Task.Delay(2000);
				Status = "Locked";
				CanLock = true;
				IsBusy = false;
			}, this.WhenAny(m=>m.CanLock, (arg) => arg.Value == true));

			Unlock = ReactiveCommand.Create(async () =>
			{
				IsBusy = true;
				CanUnlock = false;
				//TODO REST
				await Task.Delay(2000);
				Status = "Unlocked";
				CanUnlock = true;
				IsBusy = false;
			}, this.WhenAny(m => m.CanUnlock, (arg) => arg.Value == true));
		}

		string status;

		public string Status
		{
			get
			{
				return status;
			}

			set
			{
				this.RaiseAndSetIfChanged(ref status, value);
			}
		}

		public ReactiveCommand Lock { get; set; }

		public ReactiveCommand Unlock { get; set; }

		bool canLock = true;

		public bool CanLock
		{
			get { return canLock; }
			set
			{
				this.RaiseAndSetIfChanged(ref canLock, value);
			}
		}

		bool canUnlock = true;

		public bool CanUnlock
		{
			get { return canUnlock; }
			set
			{
				this.RaiseAndSetIfChanged(ref canUnlock, value);
			}
		}

		bool guardMode;

		public bool GuardMode
		{
			get
			{
				return guardMode;
			}

			set
			{

				this.RaiseAndSetIfChanged(ref guardMode, value);
			}
		}

		bool canGuard = true;

		public bool CanGuard
		{
			get { return canGuard; }
			set
			{
				this.RaiseAndSetIfChanged(ref canGuard, value);
			}
		}

		bool serviceMode;

		public bool ServiceMode
		{
			get
			{
				return serviceMode;
			}

			set
			{

				this.RaiseAndSetIfChanged(ref serviceMode, value);
			}
		}

		bool canService = true;

		public bool CanService
		{
			get { return canService; }
			set
			{
				this.RaiseAndSetIfChanged(ref canService, value);
			}
		}

		bool isBusy = false;

		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				this.RaiseAndSetIfChanged(ref isBusy, value);
			}
		}

		string token;

		public string Token
		{
			get
			{
				return token;
			}

			set
			{

				this.RaiseAndSetIfChanged(ref token, value);
			}
		}

		public ReactiveCommand SignIn { get; set; }

		public ReactiveCommand SignOut { get; set; }

		string login;

		public string Login
		{
			get
			{
				return login;
			}

			set
			{

				this.RaiseAndSetIfChanged(ref login, value);
			}
		}

		string password;

		public string Password
		{
			get
			{
				return password;
			}

			set
			{

				this.RaiseAndSetIfChanged(ref password, value);
			}
		}

		bool showLogin = true;

		public bool ShowLogin
		{
			get { return showLogin; }
			set
			{
				this.RaiseAndSetIfChanged(ref showLogin, value);
			}
		}

		bool showControl;

		public bool ShowControl
		{
			get
			{
				return showControl;
			}

			set
			{

				this.RaiseAndSetIfChanged(ref showControl, value);
			}
		}
	}
}

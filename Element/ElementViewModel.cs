using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using System.Threading;
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
					token = "token";
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
			}, this.WhenAny(m => m.Login, m => m.Password, m => m.token, (log, pas, tok) => string.IsNullOrWhiteSpace(log.Value) == false && string.IsNullOrWhiteSpace(pas.Value)==false && tok.Value == null));

			SignOut = ReactiveCommand.CreateFromTask(async () =>
			{
				//TODO Confirm from user
				var confirm = await Application.Current.MainPage.DisplayActionSheet("Are you sure you want log out?", null, null, new string[]{"Yes","No"});
				if (confirm == "Yes")
				{
					await Task.Delay(1000);
					token = null;
					Status = $"Sign out by {Login}";
				}
			}, this.WhenAny(m => m.token, (tok) => tok.Value != null));

			this.ObservableForProperty(m => m.token).Subscribe((obj) =>
			  {
				  if (token != null)
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
			});

			Unlock = ReactiveCommand.Create(async () =>
			{
				IsBusy = true;
				CanUnlock = false;
				//TODO REST
				await Task.Delay(2000);
				Status = "Unlocked";
				CanUnlock = true;
				IsBusy = false;
			});
		}

		[Reactive]
		public string Status { get; set; }

		public ReactiveCommand Lock { get; set; }

		public ReactiveCommand Unlock { get; set; }


		[Reactive]
		public bool CanLock { get; set; } = true;

		[Reactive]
		public bool CanUnlock { get; set; } = true;

		[Reactive]
		public bool GuardMode { get; set; }

		[Reactive]
		public bool CanGuard { get; set; } = true;

		[Reactive]
		public bool ServiceMode { get; set; }
		[Reactive]
		public bool CanService { get; set; } = true;

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

using MauiAppDemo.Models;
using MauiAppDemo.Utils;

namespace MauiAppDemo.Services;

public class MauiAppDemoService : IMauiAppDemoService
{
	private MauiAppDemoDbContext ctx;

	public MauiAppDemoDbContext DBContext
	{
		get
		{
			if (ctx == null)
			{
				ctx = new MauiAppDemoDbContext();

				ctx.Database.EnsureCreated();

				using (var t = ctx.Database.BeginTransaction())
				{
					var count = ctx.Users.Count();
					if (count == 0)
					{
						ctx.Users.Add(new User() { No = 1234, Name = "Kashin777", Password = Hashs.Sha256("1234", "1234") });
						ctx.Users.Add(new User() { No = 9999, Name = "User9999", Password = Hashs.Sha256("9999", "Password") });
					}

					t.Commit();
				}
			}

			return ctx;
		}
	}

	public User LoginUser
	{
		set;
		get;
	}
}

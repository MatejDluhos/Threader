using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ThreaderApp.Models;

namespace ThreaderApp.Data
{
	public class ThreaderContext : IdentityDbContext<User>
	{
		public ThreaderContext(DbContextOptions<ThreaderContext> options) : base(options) { }
	}
}

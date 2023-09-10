using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Shared_.Services
{
	public interface ISharedIdentityService
	{
		public string GetUserId { get; }
	}
}

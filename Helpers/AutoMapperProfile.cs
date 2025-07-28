using AutoMapper;
using FashionShop.Data;
using FashionShop.ViewModels;

namespace FashionShop.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Register, KhachHang>();
		}
	}
}

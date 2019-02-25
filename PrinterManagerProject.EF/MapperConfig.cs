using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrinterManagerProject.EF.Models;

namespace PrinterManagerProject.EF
{
    public class MapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<tZHY, tOrder>().ForMember(dto => dto.Id, opt => opt.Ignore());
                x.CreateMap<tOrder, OrderModel>();
                x.CreateMap<tOrder, tWarning>().ForMember(dto=>dto.OrderId,opt=>opt.MapFrom(s=>s.Id)).ForMember(dto => dto.Id, opt => opt.Ignore());
                x.CreateMap<tOrderBak, tOrder>();
                x.CreateMap<tWarningBak, tWarning>();
            });
        }
    }
}

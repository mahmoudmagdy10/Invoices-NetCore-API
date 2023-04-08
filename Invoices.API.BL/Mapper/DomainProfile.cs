using AutoMapper;
using Invoices.BL.Models;
using Invoices.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Invoice, InvoiceVM>();
            CreateMap<InvoiceVM, Invoice>();

            CreateMap<Section, SectionVM>();
            CreateMap<SectionVM, Section>();

            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product>();

            CreateMap<InvoiceAttachments, InvoiceAttachmentsVM>();
            CreateMap<InvoiceAttachmentsVM, InvoiceAttachments>();

            CreateMap<InvoicesDetails, InvoicesDetailsVM>();
            CreateMap<InvoicesDetailsVM, InvoicesDetails>();
            CreateMap<InvoiceVM, InvoicesDetailsVM>();

        }
    }
}

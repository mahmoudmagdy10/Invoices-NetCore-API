using Invoices.BL.Interface;
using Invoices.BL.Models;
using Invoices.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Invoices.BL.Helper
{
    public static class Export
    {

        public static StringBuilder Excel(IEnumerable<InvoiceVM> model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("InvoiceNumber,RateVat,Notes,User,Status,ValueStatus," +
                        "InvoiceDate,DueDate,PayDate,Discount,AmountCollection,AmountCommission,ValueVat,Total,ProductId,RestAmount,AllocatedAmount");
            foreach (var invoice in model)
            {
                stringBuilder.AppendLine($"{invoice.InvoiceNumber},{invoice.RateVat},{invoice.Notes},{invoice.User}," +
                    $"{invoice.Status},{invoice.ValueStatus},{invoice.InvoiceDate},{invoice.DueDate},{invoice.PayDate}," +
                    $"{invoice.Discount},{invoice.AmountCollection},{invoice.AmountCommission},{invoice.ValueVat},{invoice.Total},{invoice.ProductId}," +
                    $"{invoice.RestAmount},{invoice.AllocatedAmount}");
            }
            return stringBuilder;
        }

    }
}

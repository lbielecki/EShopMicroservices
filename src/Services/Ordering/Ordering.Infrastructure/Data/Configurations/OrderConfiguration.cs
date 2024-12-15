using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(c => c.CustomerId)
            .IsRequired();

        builder.HasMany(c => c.OrderItems)
            .WithOne()
            .HasForeignKey(c => c.OrderId);

        builder.ComplexProperty(
            c => c.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });

        builder.ComplexProperty(
            c => c.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(n => n.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(n => n.EmailAddress)
                    .HasMaxLength(50);
                
                addressBuilder.Property(n => n.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(n => n.Country)
                    .HasMaxLength(50);
                
                addressBuilder.Property(n => n.State)
                    .HasMaxLength(50);
                
                addressBuilder.Property(n => n.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            });
        
        builder.ComplexProperty(
            c => c.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(n => n.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();
                
                addressBuilder.Property(n => n.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(n => n.EmailAddress)
                    .HasMaxLength(50);
                
                addressBuilder.Property(n => n.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(n => n.Country)
                    .HasMaxLength(50);
                
                addressBuilder.Property(n => n.State)
                    .HasMaxLength(50);
                
                addressBuilder.Property(n => n.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            });

        builder.ComplexProperty(
            c => c.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(n => n.CardName)
                    .HasMaxLength(50);
                
                paymentBuilder.Property(n => n.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();

                paymentBuilder.Property(n => n.Expiration)
                    .HasMaxLength(10);
                
                paymentBuilder.Property(n => n.CVV)
                    .HasMaxLength(3);

                paymentBuilder.Property(n => n.PaymentMethod);
            });
        
        builder.Property(c => c.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(c => c.TotalPrice);
    }
}
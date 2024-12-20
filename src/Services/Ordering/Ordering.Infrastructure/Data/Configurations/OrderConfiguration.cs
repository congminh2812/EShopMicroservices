namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));
            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).IsRequired();
            builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);
            builder.ComplexProperty(x => x.OrderName, nameBuilder =>
            {
                nameBuilder.Property(x => x.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(x => x.ShippingAddress, nameBuilder =>
            {
                nameBuilder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                nameBuilder.Property(x => x.Country)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.State)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

            builder.ComplexProperty(x => x.BillingAddress, nameBuilder =>
            {
                nameBuilder.Property(x => x.FirstName)
               .HasMaxLength(50)
               .IsRequired();

                nameBuilder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                nameBuilder.Property(x => x.Country)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.State)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

            builder.ComplexProperty(x => x.Payment, nameBuilder =>
            {
                nameBuilder.Property(x => x.CardName)
                .HasMaxLength(50);

                nameBuilder.Property(x => x.CardNumber)
                .HasMaxLength(50)
                .IsRequired();

                nameBuilder.Property(x => x.Expiration)
                .HasMaxLength(10);

                nameBuilder.Property(x => x.Cvv)
                .HasMaxLength(3);

                nameBuilder.Property(x => x.PaymentMethod);
            });

            builder.Property(x => x.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(s => s.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(x => x.TotalPrice);
        }
    }
}

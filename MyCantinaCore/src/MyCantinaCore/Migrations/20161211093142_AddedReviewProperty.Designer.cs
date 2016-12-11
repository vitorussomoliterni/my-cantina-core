using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyCantinaCore.DataAccess.Models;

namespace MyCantinaCore.Migrations
{
    [DbContext(typeof(MyCantinaCoreDbContext))]
    [Migration("20161211093142_AddedReviewProperty")]
    partial class AddedReviewProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.Bottle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AverageRating");

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Producer")
                        .IsRequired();

                    b.Property<string>("Region");

                    b.Property<string>("WineType")
                        .IsRequired();

                    b.Property<string>("Year")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Bottles");
                });

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.BottleGrapeVariety", b =>
                {
                    b.Property<int>("BottleId");

                    b.Property<int>("GrapeVarietyId");

                    b.HasKey("BottleId", "GrapeVarietyId");

                    b.HasIndex("GrapeVarietyId");

                    b.ToTable("BottleGrapeVarieties");
                });

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.Consumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Consumers");
                });

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.ConsumerBottle", b =>
                {
                    b.Property<int>("BottleId");

                    b.Property<int>("ConsumerId");

                    b.Property<DateTime>("DateAcquired");

                    b.Property<DateTime?>("DateOpened");

                    b.Property<bool>("Owned")
                        .HasColumnType("bit");

                    b.Property<double?>("PricePaid")
                        .HasColumnType("money");

                    b.Property<int>("Qty");

                    b.HasKey("BottleId", "ConsumerId");

                    b.HasIndex("ConsumerId");

                    b.ToTable("ConsumerBottles");
                });

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.GrapeVariety", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Colour");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("GrapeVarieties");
                });

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.Review", b =>
                {
                    b.Property<int>("BottleId");

                    b.Property<int>("ConsumerId");

                    b.Property<string>("Body");

                    b.Property<DateTime?>("DateModified");

                    b.Property<DateTime>("DatePosted");

                    b.Property<int>("Rating");

                    b.HasKey("BottleId", "ConsumerId");

                    b.HasIndex("ConsumerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.BottleGrapeVariety", b =>
                {
                    b.HasOne("MyCantinaCore.DataAccess.Models.Bottle", "Bottle")
                        .WithMany("BottleGrapeVarieties")
                        .HasForeignKey("BottleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyCantinaCore.DataAccess.Models.GrapeVariety", "GrapeVariety")
                        .WithMany("BottleGrapeVarieties")
                        .HasForeignKey("GrapeVarietyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.ConsumerBottle", b =>
                {
                    b.HasOne("MyCantinaCore.DataAccess.Models.Bottle", "Bottle")
                        .WithMany("ConsumerBottles")
                        .HasForeignKey("BottleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyCantinaCore.DataAccess.Models.Consumer", "Consumer")
                        .WithMany("ConsumerBottles")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCantinaCore.DataAccess.Models.Review", b =>
                {
                    b.HasOne("MyCantinaCore.DataAccess.Models.Bottle", "Bottle")
                        .WithMany("Reviews")
                        .HasForeignKey("BottleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyCantinaCore.DataAccess.Models.Consumer", "Consumer")
                        .WithMany("Reviews")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

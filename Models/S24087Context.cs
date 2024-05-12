using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zadanie7.Models;

public partial class S24087Context : DbContext
{
    public S24087Context()
    {
    }

    public S24087Context(DbContextOptions<S24087Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Adre> Adres { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<ElementWyposazenium> ElementWyposazenia { get; set; }

    public virtual DbSet<Klient> Klients { get; set; }

    public virtual DbSet<ModelSamochodu> ModelSamochodus { get; set; }

    public virtual DbSet<Osoba> Osobas { get; set; }

    public virtual DbSet<Samochod> Samochods { get; set; }

    public virtual DbSet<Sprzedawca> Sprzedawcas { get; set; }

    public virtual DbSet<Sprzedaz> Sprzedazs { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=db-mssql16;Initial Catalog=s24087;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("adres_pk");

            entity.ToTable("adres");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.KodPoczt)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("kod_poczt");
            entity.Property(e => e.Miasto)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("miasto");
            entity.Property(e => e.NumerDomu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numer_domu");
            entity.Property(e => e.Ulica)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("ulica");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Client_pk");

            entity.ToTable("Client", "trip");

            entity.Property(e => e.IdClient).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.Property(e => e.Pesel).HasMaxLength(120);
            entity.Property(e => e.Telephone).HasMaxLength(120);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip }).HasName("Client_Trip_pk");

            entity.ToTable("Client_Trip", "trip");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Client");

            entity.HasOne(d => d.IdTripNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Trip");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("Country_pk");

            entity.ToTable("Country", "trip");

            entity.Property(e => e.IdCountry).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany(d => d.IdTrips).WithMany(p => p.IdCountries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryTrip",
                    r => r.HasOne<Trip>().WithMany()
                        .HasForeignKey("IdTrip")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Trip"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Country"),
                    j =>
                    {
                        j.HasKey("IdCountry", "IdTrip").HasName("Country_Trip_pk");
                        j.ToTable("Country_Trip", "trip");
                    });
        });

        modelBuilder.Entity<ElementWyposazenium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("element_wyposazenia_pk");

            entity.ToTable("element_wyposazenia");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("nazwa");

            entity.HasMany(d => d.SamochodVins).WithMany(p => p.ElementWyposazenia)
                .UsingEntity<Dictionary<string, object>>(
                    "Wyposazenie",
                    r => r.HasOne<Samochod>().WithMany()
                        .HasForeignKey("SamochodVin")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("wyposazenie_samochod"),
                    l => l.HasOne<ElementWyposazenium>().WithMany()
                        .HasForeignKey("ElementWyposazeniaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("wyposazenie_element_wypos"),
                    j =>
                    {
                        j.HasKey("ElementWyposazeniaId", "SamochodVin").HasName("wyposazenie_pk");
                        j.ToTable("wyposazenie");
                        j.IndexerProperty<int>("ElementWyposazeniaId").HasColumnName("element_wyposazenia_id");
                        j.IndexerProperty<string>("SamochodVin")
                            .HasMaxLength(17)
                            .IsUnicode(false)
                            .HasColumnName("samochod_vin");
                    });
        });

        modelBuilder.Entity<Klient>(entity =>
        {
            entity.HasKey(e => e.OsobaId).HasName("klient_pk");

            entity.ToTable("klient");

            entity.Property(e => e.OsobaId)
                .ValueGeneratedNever()
                .HasColumnName("osoba_id");
            entity.Property(e => e.AdresId).HasColumnName("adres_id");
            entity.Property(e => e.NrTel)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("nr_tel");
            entity.Property(e => e.Pesel)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("pesel");

            entity.HasOne(d => d.Adres).WithMany(p => p.Klients)
                .HasForeignKey(d => d.AdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("klient_Adres");

            entity.HasOne(d => d.Osoba).WithOne(p => p.Klient)
                .HasForeignKey<Klient>(d => d.OsobaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("klient_osoba");

            entity.HasMany(d => d.Sprzedazs).WithMany(p => p.Klients)
                .UsingEntity<Dictionary<string, object>>(
                    "Wspolwlasciciele",
                    r => r.HasOne<Sprzedaz>().WithMany()
                        .HasForeignKey("SprzedazId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("wspolwlasciciele_sprzedaz"),
                    l => l.HasOne<Klient>().WithMany()
                        .HasForeignKey("KlientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("wspolwlasciciele_klient"),
                    j =>
                    {
                        j.HasKey("KlientId", "SprzedazId").HasName("wspolwlasciciele_pk");
                        j.ToTable("wspolwlasciciele");
                        j.IndexerProperty<int>("KlientId").HasColumnName("klient_id");
                        j.IndexerProperty<int>("SprzedazId").HasColumnName("sprzedaz_id");
                    });
        });

        modelBuilder.Entity<ModelSamochodu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("model_samochodu_pk");

            entity.ToTable("model_samochodu");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Marka)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("marka");
            entity.Property(e => e.Model)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("model");
        });

        modelBuilder.Entity<Osoba>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("osoba_pk");

            entity.ToTable("osoba");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Imie)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("imie");
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("nazwisko");
        });

        modelBuilder.Entity<Samochod>(entity =>
        {
            entity.HasKey(e => e.Vin).HasName("samochod_pk");

            entity.ToTable("samochod");

            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("vin");
            entity.Property(e => e.CenaZakupu).HasColumnName("cena_zakupu");
            entity.Property(e => e.DataZakupu)
                .HasColumnType("date")
                .HasColumnName("data_zakupu");
            entity.Property(e => e.ModelSamochoduId).HasColumnName("model_samochodu_id");
            entity.Property(e => e.PojSilnika).HasColumnName("poj_silnika");
            entity.Property(e => e.Przebieg).HasColumnName("przebieg");
            entity.Property(e => e.RokProdukcji).HasColumnName("rok_produkcji");

            entity.HasOne(d => d.ModelSamochodu).WithMany(p => p.Samochods)
                .HasForeignKey(d => d.ModelSamochoduId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("samochod_model_samochodu");
        });

        modelBuilder.Entity<Sprzedawca>(entity =>
        {
            entity.HasKey(e => e.OsobaId).HasName("sprzedawca_pk");

            entity.ToTable("sprzedawca");

            entity.Property(e => e.OsobaId)
                .ValueGeneratedNever()
                .HasColumnName("osoba_id");
            entity.Property(e => e.Prowizja).HasColumnName("prowizja");

            entity.HasOne(d => d.Osoba).WithOne(p => p.Sprzedawca)
                .HasForeignKey<Sprzedawca>(d => d.OsobaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprzedawca_osoba");
        });

        modelBuilder.Entity<Sprzedaz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sprzedaz_pk");

            entity.ToTable("sprzedaz", tb =>
                {
                    tb.HasTrigger("blokowanie_modyfikacji_i_usuwania_sprzedazy");
                    tb.HasTrigger("sprawdz_date_sprzedazy");
                    tb.HasTrigger("zablokowanie_sprzedazy_samemu_sobie");
                });

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cena).HasColumnName("cena");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.SamochodVin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("samochod_vin");
            entity.Property(e => e.SprzedawcaId).HasColumnName("sprzedawca_id");

            entity.HasOne(d => d.SamochodVinNavigation).WithMany(p => p.Sprzedazs)
                .HasForeignKey(d => d.SamochodVin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprzedaz_samochod");

            entity.HasOne(d => d.Sprzedawca).WithMany(p => p.Sprzedazs)
                .HasForeignKey(d => d.SprzedawcaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprzedaz_sprzedawca");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("Trip_pk");

            entity.ToTable("Trip", "trip");

            entity.Property(e => e.IdTrip).ValueGeneratedNever();
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

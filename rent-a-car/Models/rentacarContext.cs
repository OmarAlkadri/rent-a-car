using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace rent_a_car
{
    public partial class rentacarContext : DbContext
    {
        public rentacarContext()
        {
        }

        public rentacarContext(DbContextOptions<rentacarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Adre> Adres { get; set; }
        public virtual DbSet<Araba> Arabas { get; set; }
        public virtual DbSet<ArabaFirma> ArabaFirmas { get; set; }
        public virtual DbSet<ArabaKira> ArabaKiras { get; set; }
        public virtual DbSet<Fotograf> Fotografs { get; set; }
        public virtual DbSet<IlanKoy> IlanKoys { get; set; }
        public virtual DbSet<Kiraci> Kiracis { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Ofi> Ofis { get; set; }
        public virtual DbSet<Ozellik> Ozelliks { get; set; }
        public virtual DbSet<OzellikEkle> OzellikEkles { get; set; }
        public virtual DbSet<Personel> Personels { get; set; }
        public virtual DbSet<ServisFirma> ServisFirmas { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=rent-a-car;Username=postgres;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.Id)
                    .HasConstraintName("user_admin");
            });

            modelBuilder.Entity<Adre>(entity =>
            {
                entity.ToTable("adres");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Il)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("il");

                entity.Property(e => e.Ilce)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("ilce");

                entity.Property(e => e.Satir1)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("satir1");

                entity.Property(e => e.Satir2)
                    .HasMaxLength(1)
                    .HasColumnName("satir2");
            });

            modelBuilder.Entity<Araba>(entity =>
            {
                entity.ToTable("araba");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("ad");

                entity.Property(e => e.ArabaFirId).HasColumnName("araba_fir_id");

                entity.Property(e => e.ServisId).HasColumnName("servis_id");

                entity.HasOne(d => d.ArabaFir)
                    .WithMany(p => p.Arabas)
                    .HasForeignKey(d => d.ArabaFirId)
                    .HasConstraintName("arabafirma_araba");

                entity.HasOne(d => d.Servis)
                    .WithMany(p => p.Arabas)
                    .HasForeignKey(d => d.ServisId)
                    .HasConstraintName("servisfirma_araba");
            });

            modelBuilder.Entity<ArabaFirma>(entity =>
            {
                entity.ToTable("araba_firma");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("ad");

                entity.Property(e => e.AdresId).HasColumnName("adres_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("email");

                entity.Property(e => e.FirmaSahibi)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("firma_sahibi");

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("telefon");

                entity.HasOne(d => d.Adres)
                    .WithMany(p => p.ArabaFirmas)
                    .HasForeignKey(d => d.AdresId)
                    .HasConstraintName("adres_arabafirma");
            });

            modelBuilder.Entity<ArabaKira>(entity =>
            {
                entity.ToTable("araba_kira");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ArabaId).HasColumnName("araba_id");

                entity.Property(e => e.KiraFiyati).HasColumnName("kira_fiyati");

                entity.Property(e => e.KiraciId).HasColumnName("kiraci_id");

                entity.Property(e => e.PersonelId).HasColumnName("personel_id");

                entity.Property(e => e.Sure)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("sure");

                entity.HasOne(d => d.Araba)
                    .WithMany(p => p.ArabaKiras)
                    .HasForeignKey(d => d.ArabaId)
                    .HasConstraintName("araba_arabakira");

                entity.HasOne(d => d.Kiraci)
                    .WithMany(p => p.ArabaKiras)
                    .HasForeignKey(d => d.KiraciId)
                    .HasConstraintName("kiraci_arabakira");

                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.ArabaKiras)
                    .HasForeignKey(d => d.PersonelId)
                    .HasConstraintName("personel_arabakira");
            });

            modelBuilder.Entity<Fotograf>(entity =>
            {
                entity.ToTable("fotograf");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ArabaId).HasColumnName("araba_id");

                entity.Property(e => e.Fotograf1)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("fotograf");

                entity.HasOne(d => d.Araba)
                    .WithMany(p => p.Fotografs)
                    .HasForeignKey(d => d.ArabaId)
                    .HasConstraintName("araba_fotograf");
            });

            modelBuilder.Entity<IlanKoy>(entity =>
            {
                entity.ToTable("ilan_koy");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ArabaId).HasColumnName("araba_id");

                entity.Property(e => e.Fiyat).HasColumnName("fiyat");

                entity.Property(e => e.PersonelId).HasColumnName("personel_id");

                entity.Property(e => e.Tarih)
                    .HasColumnType("date")
                    .HasColumnName("tarih");

                entity.HasOne(d => d.Araba)
                    .WithMany(p => p.IlanKoys)
                    .HasForeignKey(d => d.ArabaId)
                    .HasConstraintName("araba_ilankoy");

                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.IlanKoys)
                    .HasForeignKey(d => d.PersonelId)
                    .HasConstraintName("personel_ilankoy");
            });

            modelBuilder.Entity<Kiraci>(entity =>
            {
                entity.ToTable("kiraci");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("ad");

                entity.Property(e => e.Soyad)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("soyad");

                entity.Property(e => e.Yas).HasColumnName("yas");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("login");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Tarih)
                    .HasColumnType("date")
                    .HasColumnName("tarih");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_login");
            });

            modelBuilder.Entity<Ofi>(entity =>
            {
                entity.ToTable("ofis");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<Ozellik>(entity =>
            {
                entity.ToTable("ozellik");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.OzellikTipi)
                    .HasMaxLength(1)
                    .HasColumnName("ozellik_tipi");
            });

            modelBuilder.Entity<OzellikEkle>(entity =>
            {
                entity.ToTable("ozellik_ekle");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ArabaId).HasColumnName("araba_id");

                entity.Property(e => e.OzellikId).HasColumnName("ozellik_id");

                entity.Property(e => e.Tarih)
                    .HasColumnType("date")
                    .HasColumnName("tarih");

                entity.HasOne(d => d.Araba)
                    .WithMany(p => p.OzellikEkles)
                    .HasForeignKey(d => d.ArabaId)
                    .HasConstraintName("araba_ozellikekle");

                entity.HasOne(d => d.Ozellik)
                    .WithMany(p => p.OzellikEkles)
                    .HasForeignKey(d => d.OzellikId)
                    .HasConstraintName("ozellik_ozellikekle");
            });

            modelBuilder.Entity<Personel>(entity =>
            {
                entity.ToTable("personel");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.OfisId).HasColumnName("ofis_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Personel)
                    .HasForeignKey<Personel>(d => d.Id)
                    .HasConstraintName("user_personel");

                entity.HasOne(d => d.Ofis)
                    .WithMany(p => p.Personels)
                    .HasForeignKey(d => d.OfisId)
                    .HasConstraintName("ofis_personel");
            });

            modelBuilder.Entity<ServisFirma>(entity =>
            {
                entity.ToTable("servis_firma");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("ad");

                entity.Property(e => e.Servis)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("servis");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Id, "unique_user_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("ad");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("email");

                entity.Property(e => e.KisiTuru)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("kisi_turu");

                entity.Property(e => e.Password)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("password");

                entity.Property(e => e.Soyad)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("soyad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

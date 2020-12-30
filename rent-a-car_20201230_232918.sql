--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1
-- Dumped by pg_dump version 13.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: rent-a-car; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "rent-a-car" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'English_United States.1252';


ALTER DATABASE "rent-a-car" OWNER TO postgres;

\connect -reuse-previous=on "dbname='rent-a-car'"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: ad_soyad_birlestir(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.ad_soyad_birlestir() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  new."ad_soyad" = concat(new.ad,' ',new.soyad);
  RETURN NEW;
END;
$$;


ALTER FUNCTION public.ad_soyad_birlestir() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: araba; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.araba (
    id integer NOT NULL,
    araba_fir_id integer NOT NULL,
    ad character varying NOT NULL,
    servis_id integer NOT NULL,
    created_at timestamp with time zone DEFAULT now() NOT NULL,
    updated_at timestamp with time zone DEFAULT now() NOT NULL,
    indirimli_fiyat integer,
    indirim_orani integer,
    kira_fiyati integer
);


ALTER TABLE public.araba OWNER TO postgres;

--
-- Name: get_araba_by_firma(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_araba_by_firma(_firma character varying) RETURNS SETOF public.araba
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT *
                   FROM araba
                  WHERE (select araba_firma.ad from araba_firma where araba_firma.id = araba.araba_fir_id ) like '%' || _firma || '%';

    IF NOT FOUND THEN
        RAISE EXCEPTION '% firmali bir araba yok.', _firma;
    END IF;

    RETURN;
 END;
$$;


ALTER FUNCTION public.get_araba_by_firma(_firma character varying) OWNER TO postgres;

--
-- Name: get_araba_under(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_araba_under(i integer) RETURNS SETOF public.araba
    LANGUAGE plpgsql
    AS $_$
BEGIN
    RETURN QUERY SELECT *
                   FROM araba
                  WHERE araba.kira_fiyati <= $1;

    IF NOT FOUND THEN
        RAISE EXCEPTION '% altinda bir araba yok.', $1;
    END IF;

    RETURN;
 END;
$_$;


ALTER FUNCTION public.get_araba_under(i integer) OWNER TO postgres;

--
-- Name: get_gunluk_kira(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_gunluk_kira(araba_id integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$

declare gunluk_kira int;
BEGIN
    
    gunluk_kira := ((SELECT kira_fiyati 
                   FROM araba
                  WHERE araba.id = araba_id) / 30)::float;
    

    RETURN  gunluk_kira ;
 END;
$$;


ALTER FUNCTION public.get_gunluk_kira(araba_id integer) OWNER TO postgres;

--
-- Name: get_yillik_kira(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_yillik_kira(araba_id integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$

declare yillik_kira int;
BEGIN
    
    yillik_kira := (SELECT kira_fiyati 
                   FROM araba
                  WHERE araba.id = araba_id) * 12;
    


    RETURN  yillik_kira ;
 END;
$$;


ALTER FUNCTION public.get_yillik_kira(araba_id integer) OWNER TO postgres;

--
-- Name: indirim_hesapla(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.indirim_hesapla() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  NEW."indirimli_fiyat" = (100 - NEW.indirim_orani )::float/ 100 * NEW.kira_fiyati;
  RETURN NEW;
END;
$$;


ALTER FUNCTION public.indirim_hesapla() OWNER TO postgres;

--
-- Name: trigger_set_timestamp(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.trigger_set_timestamp() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  NEW.updated_at = NOW();
  RETURN NEW;
END;
$$;


ALTER FUNCTION public.trigger_set_timestamp() OWNER TO postgres;

--
-- Name: user_insert_trigger_fnc(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.user_insert_trigger_fnc() RETURNS trigger
    LANGUAGE plpgsql
    AS $$

BEGIN



    INSERT INTO "user_yedek" ( "id", "soyad", "ad" ,"time")

         VALUES(NEW."id",NEW."soyad",NEW."ad",current_date);



RETURN NEW;

END;

$$;


ALTER FUNCTION public.user_insert_trigger_fnc() OWNER TO postgres;

--
-- Name: admin; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.admin (
    id integer NOT NULL
);


ALTER TABLE public.admin OWNER TO postgres;

--
-- Name: adres; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.adres (
    id integer NOT NULL,
    ilce character varying NOT NULL,
    il character varying NOT NULL,
    satir1 character varying NOT NULL,
    satir2 character varying
);


ALTER TABLE public.adres OWNER TO postgres;

--
-- Name: araba_firma; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.araba_firma (
    id integer NOT NULL,
    ad character varying NOT NULL,
    firma_sahibi character varying NOT NULL,
    telefon character varying NOT NULL,
    email character varying NOT NULL,
    adres_id integer NOT NULL
);


ALTER TABLE public.araba_firma OWNER TO postgres;

--
-- Name: araba_kira; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.araba_kira (
    id integer NOT NULL,
    personel_id integer NOT NULL,
    araba_id integer NOT NULL,
    kiraci_id integer NOT NULL,
    kira_fiyati integer NOT NULL,
    sure character varying NOT NULL
);


ALTER TABLE public.araba_kira OWNER TO postgres;

--
-- Name: fotograf; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.fotograf (
    id integer NOT NULL,
    araba_id integer NOT NULL,
    fotograf character varying NOT NULL
);


ALTER TABLE public.fotograf OWNER TO postgres;

--
-- Name: ilan_koy; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ilan_koy (
    id integer NOT NULL,
    araba_id integer NOT NULL,
    personel_id integer NOT NULL,
    tarih date NOT NULL,
    fiyat integer NOT NULL
);


ALTER TABLE public.ilan_koy OWNER TO postgres;

--
-- Name: kiraci; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kiraci (
    id integer NOT NULL,
    ad character varying NOT NULL,
    soyad character varying NOT NULL,
    yas integer NOT NULL
);


ALTER TABLE public.kiraci OWNER TO postgres;

--
-- Name: login; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.login (
    id integer NOT NULL,
    user_id integer NOT NULL,
    tarih date NOT NULL
);


ALTER TABLE public.login OWNER TO postgres;

--
-- Name: ofis; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ofis (
    id integer NOT NULL,
    ad character varying NOT NULL
);


ALTER TABLE public.ofis OWNER TO postgres;

--
-- Name: ozellik; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ozellik (
    id integer NOT NULL,
    ozellik_tipi character varying NOT NULL
);


ALTER TABLE public.ozellik OWNER TO postgres;

--
-- Name: ozellik_ekle; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ozellik_ekle (
    id integer NOT NULL,
    araba_id integer NOT NULL,
    ozellik_id integer NOT NULL,
    tarih date NOT NULL
);


ALTER TABLE public.ozellik_ekle OWNER TO postgres;

--
-- Name: personel; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.personel (
    id integer NOT NULL,
    ofis_id integer NOT NULL
);


ALTER TABLE public.personel OWNER TO postgres;

--
-- Name: servis_firma; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.servis_firma (
    id integer NOT NULL,
    ad character varying NOT NULL,
    servis character varying NOT NULL
);


ALTER TABLE public.servis_firma OWNER TO postgres;

--
-- Name: user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."user" (
    id integer NOT NULL,
    password character varying NOT NULL,
    ad character varying NOT NULL,
    soyad character varying NOT NULL,
    email character varying NOT NULL,
    kisi_turu character varying NOT NULL,
    ad_soyad character varying NOT NULL
);


ALTER TABLE public."user" OWNER TO postgres;

--
-- Name: user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.user_id_seq OWNER TO postgres;

--
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;


--
-- Name: user_password_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.user_password_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.user_password_seq OWNER TO postgres;

--
-- Name: user_password_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.user_password_seq OWNED BY public."user".password;


--
-- Name: user_yedek; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.user_yedek (
    id integer NOT NULL,
    ad character varying NOT NULL,
    soyad character varying NOT NULL,
    "tıme" timestamp with time zone NOT NULL
);


ALTER TABLE public.user_yedek OWNER TO postgres;

--
-- Name: user id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);


--
-- Name: user password; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user" ALTER COLUMN password SET DEFAULT nextval('public.user_password_seq'::regclass);


--
-- Data for Name: admin; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: adres; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.adres VALUES
	(1, 'istanbul', 'istanbul', 'esenyurt', NULL),
	(2, 'sakarya', 'sakarya', 'serdivan', NULL),
	(3, 'istanbul ', 'istanbul', 'kadikoy', NULL),
	(4, 'istanbul', 'istanbul', 'eskudar', NULL),
	(5, 'sakarya', 'sakarya', 'camili', NULL);


--
-- Data for Name: araba; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.araba VALUES
	(1, 1, '740', 1, '2020-12-30 22:32:06.516834+03', '2020-12-30 22:32:06.516834+03', NULL, NULL, NULL),
	(4, 2, 'xt20', 1, '2020-12-30 22:32:06.516834+03', '2020-12-30 22:32:06.516834+03', NULL, NULL, NULL),
	(3, 2, '550', 2, '2020-12-30 22:32:06.516834+03', '2020-12-30 22:32:06.516834+03', 211, 10, 234),
	(2, 1, '420', 3, '2020-12-30 22:32:06.516834+03', '2020-12-30 22:32:06.516834+03', 300, 40, 500);


--
-- Data for Name: araba_firma; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.araba_firma VALUES
	(2, 'WUDi', 'Mr. Scoufild', '0553742345', 'bbb@gmail.com', 3),
	(1, 'BMW', 'Dr. Joun', '0505214435', 'aaa@gmail.com', 1),
	(3, 'WUKSVAGIN', 'Dr. Musa', '0538542286', 'ccc@gmail.com', 4);


--
-- Data for Name: araba_kira; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: fotograf; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: ilan_koy; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: kiraci; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: login; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: ofis; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: ozellik; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: ozellik_ekle; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: personel; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: servis_firma; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.servis_firma VALUES
	(1, 'BMW', 'tamir'),
	(2, 'auidi', 'tamir'),
	(3, 'BMW', 'tamir');


--
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."user" VALUES
	(1, '123', 'abdullah', 'jamous', 'ad@123.com', '1', 'abdullah jamous');


--
-- Data for Name: user_yedek; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_id_seq', 1, true);


--
-- Name: user_password_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_password_seq', 1, false);


--
-- Name: admin admin_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT admin_pkey PRIMARY KEY (id);


--
-- Name: adres unique_adres_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adres
    ADD CONSTRAINT unique_adres_id PRIMARY KEY (id);


--
-- Name: araba_firma unique_araba_firma_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba_firma
    ADD CONSTRAINT unique_araba_firma_id PRIMARY KEY (id);


--
-- Name: araba unique_araba_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba
    ADD CONSTRAINT unique_araba_id PRIMARY KEY (id);


--
-- Name: araba_kira unique_araba_kira_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba_kira
    ADD CONSTRAINT unique_araba_kira_id PRIMARY KEY (id);


--
-- Name: fotograf unique_fotograf_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fotograf
    ADD CONSTRAINT unique_fotograf_id PRIMARY KEY (id);


--
-- Name: ilan_koy unique_ilan_koy_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilan_koy
    ADD CONSTRAINT unique_ilan_koy_id PRIMARY KEY (id);


--
-- Name: kiraci unique_kiraci_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kiraci
    ADD CONSTRAINT unique_kiraci_id PRIMARY KEY (id);


--
-- Name: login unique_login_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.login
    ADD CONSTRAINT unique_login_id PRIMARY KEY (id);


--
-- Name: ofis unique_ofis_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ofis
    ADD CONSTRAINT unique_ofis_id PRIMARY KEY (id);


--
-- Name: ozellik_ekle unique_ozellik_ekle_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ozellik_ekle
    ADD CONSTRAINT unique_ozellik_ekle_id PRIMARY KEY (id);


--
-- Name: ozellik unique_ozellik_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ozellik
    ADD CONSTRAINT unique_ozellik_id PRIMARY KEY (id);


--
-- Name: personel unique_personel_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personel
    ADD CONSTRAINT unique_personel_id PRIMARY KEY (id);


--
-- Name: servis_firma unique_servis_firma_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.servis_firma
    ADD CONSTRAINT unique_servis_firma_id PRIMARY KEY (id);


--
-- Name: user unique_user_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT unique_user_id UNIQUE (id);


--
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);


--
-- Name: user ad_soyad_birle; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER ad_soyad_birle BEFORE INSERT OR UPDATE ON public."user" FOR EACH ROW EXECUTE FUNCTION public.ad_soyad_birlestir();


--
-- Name: araba indirim_hesap; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER indirim_hesap BEFORE INSERT OR UPDATE ON public.araba FOR EACH ROW EXECUTE FUNCTION public.indirim_hesapla();


--
-- Name: user set_timestamp; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER set_timestamp BEFORE UPDATE ON public."user" FOR EACH ROW EXECUTE FUNCTION public.trigger_set_timestamp();


--
-- Name: user user_insert_trigger; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER user_insert_trigger AFTER INSERT ON public."user" FOR EACH ROW EXECUTE FUNCTION public.user_insert_trigger_fnc();


--
-- Name: araba_firma adres_arabafirma; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba_firma
    ADD CONSTRAINT adres_arabafirma FOREIGN KEY (adres_id) REFERENCES public.adres(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: araba_kira araba_arabakira; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba_kira
    ADD CONSTRAINT araba_arabakira FOREIGN KEY (araba_id) REFERENCES public.araba(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: fotograf araba_fotograf; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fotograf
    ADD CONSTRAINT araba_fotograf FOREIGN KEY (araba_id) REFERENCES public.araba(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: ilan_koy araba_ilankoy; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilan_koy
    ADD CONSTRAINT araba_ilankoy FOREIGN KEY (araba_id) REFERENCES public.araba(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: ozellik_ekle araba_ozellikekle; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ozellik_ekle
    ADD CONSTRAINT araba_ozellikekle FOREIGN KEY (araba_id) REFERENCES public.araba(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: araba arabafirma_araba; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba
    ADD CONSTRAINT arabafirma_araba FOREIGN KEY (araba_fir_id) REFERENCES public.araba_firma(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: araba_kira kiraci_arabakira; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba_kira
    ADD CONSTRAINT kiraci_arabakira FOREIGN KEY (kiraci_id) REFERENCES public.kiraci(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: personel ofis_personel; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personel
    ADD CONSTRAINT ofis_personel FOREIGN KEY (ofis_id) REFERENCES public.ofis(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: ozellik_ekle ozellik_ozellikekle; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ozellik_ekle
    ADD CONSTRAINT ozellik_ozellikekle FOREIGN KEY (ozellik_id) REFERENCES public.ozellik(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: araba_kira personel_arabakira; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba_kira
    ADD CONSTRAINT personel_arabakira FOREIGN KEY (personel_id) REFERENCES public.personel(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: ilan_koy personel_ilankoy; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilan_koy
    ADD CONSTRAINT personel_ilankoy FOREIGN KEY (personel_id) REFERENCES public.personel(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: araba servisfirma_araba; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.araba
    ADD CONSTRAINT servisfirma_araba FOREIGN KEY (servis_id) REFERENCES public.servis_firma(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: admin user_admin; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT user_admin FOREIGN KEY (id) REFERENCES public."user"(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: login user_login; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.login
    ADD CONSTRAINT user_login FOREIGN KEY (user_id) REFERENCES public."user"(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: personel user_personel; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personel
    ADD CONSTRAINT user_personel FOREIGN KEY (id) REFERENCES public."user"(id) MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--


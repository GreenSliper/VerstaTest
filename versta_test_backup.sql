PGDMP                         y            versta_test    13.1    13.1     ?           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ?           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ?           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ?           1262    16830    versta_test    DATABASE     h   CREATE DATABASE versta_test WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';
    DROP DATABASE versta_test;
                postgres    false            ?           0    0    DATABASE versta_test    COMMENT     6   COMMENT ON DATABASE versta_test IS 'Orders database';
                   postgres    false    2993            ?            1255    16867    pseudo_encrypt(integer)    FUNCTION     ?  CREATE FUNCTION public.pseudo_encrypt(value integer) RETURNS integer
    LANGUAGE plpgsql IMMUTABLE STRICT
    AS $$
DECLARE
l1 int;
l2 int;
r1 int;
r2 int;
i int:=0;
BEGIN
 l1:= (value >> 16) & 65535;
 r1:= value & 65535;
 WHILE i < 3 LOOP
   l2 := r1;
   r2 := l1 # ((((1366 * r1 + 150889) % 714025) / 714025.0) * 32767)::int;
   l1 := l2;
   r1 := r2;
   i := i + 1;
 END LOOP;
 return ((r1 << 16) + l1);
END;
$$;
 4   DROP FUNCTION public.pseudo_encrypt(value integer);
       public          postgres    false            ?            1259    16857    orders    TABLE     |  CREATE TABLE public.orders (
    id integer NOT NULL,
    sendercity character varying(40) NOT NULL,
    senderaddress character varying(150) NOT NULL,
    receivercity character varying(40) NOT NULL,
    receiveraddress character varying(150) NOT NULL,
    packageweight numeric NOT NULL,
    receivedate date NOT NULL,
    creationtime timestamp with time zone DEFAULT now()
);
    DROP TABLE public.orders;
       public         heap    postgres    false            ?            1259    16855    orders_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.orders_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.orders_id_seq;
       public          postgres    false    201            ?           0    0    orders_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.orders_id_seq OWNED BY public.orders.id;
          public          postgres    false    200            %           2604    16868 	   orders id    DEFAULT     ?   ALTER TABLE ONLY public.orders ALTER COLUMN id SET DEFAULT public.pseudo_encrypt((nextval('public.orders_id_seq'::regclass))::integer);
 8   ALTER TABLE public.orders ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    200    201    202    201            ?          0    16857    orders 
   TABLE DATA           ?   COPY public.orders (id, sendercity, senderaddress, receivercity, receiveraddress, packageweight, receivedate, creationtime) FROM stdin;
    public          postgres    false    201   ?       ?           0    0    orders_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.orders_id_seq', 19, true);
          public          postgres    false    200            '           2606    16866    orders orders_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_pkey;
       public            postgres    false    201            ?   i   x?Eʱ
?0?9????r?6???H???%/(??C?ng8!݃V%?z???/ˋ?ʼ,?f?01o,???&?N?????p=zm[?_?I?I????{?? ?     
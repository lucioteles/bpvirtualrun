create database bprun;
use bprun;

create table Usuarios
(
	id int not null primary key auto_increment,
	login varchar(100) not null,
	senha varchar(500) not null,
	dtCadastro datetime not null,
	admin bool not null
);

insert into Usuarios(login, senha, dtCadastro, admin) values('admin', 'BA4B085E399B107C4E2A600BDF90B9A0', '2017-12-02', true);
--senha: bpivia2017

create table Campanhas
(
	id int not null primary key auto_increment,
	nome varchar(250) not null,
	dtCriacao datetime,
	dtInicioInscricao datetime not null,
	dtFimInscricao datetime not null,
	qtMinInscritos int not null,
	qtMaxInscritos int not null,
	valorInscricao decimal(10,2) not null,
	is5km bool,
	is10km bool,
	is15km bool
);

/*create table KMCampanha
(
	id int not null primary key auto_increment,
	idCampanha int not null,
	distancia int not null,
	foreign key(idCampanha) references campanhas(id)
);*/

create table Atleta
(
	Id int not null primary key auto_increment,
	IdUsuario int not null,
	Nome varchar(400) not null,
	Sexo varchar(1) not null,
	Telefone varchar(20) not null,
	Email varchar(150) not null,
	DataNascimento datetime not null,
	DataInscricao datetime not null,
	Cep varchar(20) not null,
	Logradouro varchar(400) not null,
	Complemento varchar(200),
	Numero  varchar(20) not null,
	Bairro varchar(200) not null,
	Cidade varchar(200) not null,
	UF varchar(2) not null,
	foreign key (idUsuario) references Usuarios(id)	
);

create table Inscricoes
(
	Id int not null primary key auto_increment,
	IdAtleta int not null,
	IdCampanha int not null,
	is5KM bool,
	is10KM bool,
	is15KM bool,
	foreign key (IdAtleta) references Atleta(id),
	foreign key (IdCampanha) references Campanhas(id)
);

create table Atividades
(
	Id int not null primary key auto_increment,
	idInscricao int not null,
	distancia int not null,
	dtRegistro datetime not null,
	tempo varchar(10) not null,
	localEvento varchar(200) not null,
	foreign key (idInscricao) references Inscricoes(id)
);

select * from Usuarios;
select * from Atleta;

drop table Atleta;
drop table Usuarios;
drop table Inscricoes;
select * from Inscricoes;
select * from Atividades;
select * from campanhas
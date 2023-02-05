CREATE TABLE "Carrinho" (
	"idCarrinho"	int NOT NULL,
	"idProduto"	int NOT NULL,
	"idPedido"	int NOT NULL,
	"quantidade"	int NOT NULL,
	"vlrTotal"	float NOT NULL,
	PRIMARY KEY("idCarrinho")
);
CREATE TABLE "Endereço" (
	"idEndereco"	int NOT NULL,
	"Rua"	varchar NOT NULL,
	"Numero"	int NOT NULL,
	"Bairro"	varchar NOT NULL,
	"Cidade"	varchar NOT NULL,
	"Estado"	varchar NOT NULL,
	"CEP"	int NOT NULL,
	"Complemento"	varchar,
	PRIMARY KEY("idEndereco")
);
CREATE TABLE "Entrega" (
	"idEntrega"	int NOT NULL,
	"dataEntrega"	date NOT NULL,
	"idPedido"	int NOT NULL,
	"endereço"	varchar NOT NULL,
	PRIMARY KEY("idEntrega")
);
CREATE TABLE "ItemPedido" (
	"quant"	int NOT NULL,
	"vlrUnitario"	float NOT NULL,
	"vlrTotalItem"	float NOT NULL
);
CREATE TABLE "Mensagem" (
	"conteudo"	varchar,
	"destinatario"	varchar,
	"dtEnvio"	date
);CREATE TABLE "Pagamento" (
	"idPagameno"	int NOT NULL,
	"dataPagamento"	date NOT NULL,
	"tipoPagamento"	varchar NOT NULL,
	"vlrPagamento"	float NOT NULL,
	PRIMARY KEY("idPagameno")
);
CREATE TABLE "Pedido" (
	"idPedido"	int NOT NULL,
	"dataPedido"	date NOT NULL,
	"vlrPedido"	float NOT NULL,
	PRIMARY KEY ("idPedido")
);
CREATE TABLE "Produto" (
	"idProduto"	int NOT NULL,
	"nomeProduto"	varchar NOT NULL,
	"quantEstoq"	int NOT NULL,
	"vlrProduto"	float NOT NULL,
	"unidade"	int NOT NULL,
	"peso"	float,
	PRIMARY KEY ("idProduto")
);
CREATE TABLE "Usuário" (
	"Nome"	varchar NOT NULL,
	"CPF/CNPJ"	int NOT NULL,
	"Email"	varchar NOT NULL,
	"Telefone"	int NOT NULL,
	"Idade"	char NOT NULL,
	"dataNascimento"	date NOT NULL,
	"Login"	varchar NOT NULL,
	"Senha"	varchar NOT NULL,
	PRIMARY KEY("CPF/CNPJ")
);
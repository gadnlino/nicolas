DROP TABLE IF EXISTS "Produto" ;
DROP TABLE IF EXISTS "Carrinho" ;
DROP TABLE IF EXISTS "CarrinhoProduto" ;
DROP TABLE IF EXISTS "Pedido" ;

CREATE TABLE "Produto" (
    "idProduto"    int NOT NULL IDENTITY,
    "nomeProduto"    varchar (50) NOT NULL,
    "quantEstoq"    int NOT NULL,
    "vlrProduto"    float NOT NULL,
    "peso"    float,
    PRIMARY KEY ("idProduto")
);

CREATE TABLE "Carrinho" (
    "idCarrinho"    int NOT NULL IDENTITY,
    "dataCriacao" datetime not null,
	"pedidoEfetuado" int not null,
    PRIMARY KEY("idCarrinho")
);

CREATE TABLE "CarrinhoProduto" (
    "idCarrinho"    int NOT NULL,
    "idProduto"    int NOT NULL,
	"quantidade" float not null,
	"vlrUnitarioProduto" float not null,
    PRIMARY KEY("idCarrinho", "idProduto")
);

CREATE TABLE "Pedido" (
    "idPedido"    int NOT NULL IDENTITY,
	"idCarrinho" int not null,
    "dataPedido"    datetime NOT NULL,
    "vlrPedido"    float NOT NULL,
	"quantidadeProdutos" int not null,
    PRIMARY KEY ("idPedido")
);
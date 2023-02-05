DROP TABLE IF EXISTS "Produto" ;
DROP TABLE IF EXISTS "Carrinho" ;
DROP TABLE IF EXISTS "ItemPedido" ;
DROP TABLE IF EXISTS "Pagamento" ;
DROP TABLE IF EXISTS "Pedido" ;
DROP TABLE IF EXISTS "Usuario" ;

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
    "idProduto"    int NOT NULL,
    "idPedido"    int NOT NULL,
    "quantidade"    int NOT NULL,
    "vlrTotal"    float NOT NULL,
    PRIMARY KEY("idCarrinho")
);

CREATE TABLE "ItemPedido" (
    "quant"     int NOT NULL,
    "vlrUnitario"    float NOT NULL,
    "vlrTotalItem"    float NOT NULL
);

CREATE TABLE "Pagamento" (
    "idPagamento"    int NOT NULL IDENTITY,
    "dataPagamento"    date NOT NULL,
    "tipoPagamento"    varchar (50) NOT NULL,
    "vlrPagamento"    float NOT NULL,
    PRIMARY KEY("idPagamento")
);

CREATE TABLE "Pedido" (
    "idPedido"    int NOT NULL IDENTITY,
    "dataPedido"    date NOT NULL,
    "vlrPedido"    float NOT NULL,
    PRIMARY KEY ("idPedido")
);

CREATE TABLE "Usuario" (
    "nome"    varchar (50) NOT NULL,
    "cpfCnpj"    int NOT NULL,
    "email"    varchar (50) NOT NULL,
    "telefone"    int NOT NULL,
    "idade"    char (2) NOT NULL,
    "dataNascimento"    date NOT NULL,
    "login"    varchar (50) NOT NULL,
    "senha"    varchar (50) NOT NULL,
    PRIMARY KEY("cpfCnpj")
);
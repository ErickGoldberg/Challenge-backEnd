-- Criar tabela "receitas"
CREATE TABLE receitas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    descricao VARCHAR(50) NOT NULL,
    valor NUMERIC(10, 2) NOT NULL,
    data DATE NOT NULL
);

-- Criar tabela "despesas"
CREATE TABLE despesas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    descricao VARCHAR(50) NOT NULL,
    valor NUMERIC(10, 2) NOT NULL,
    data DATE NOT NULL
);

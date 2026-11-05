-- ====================================================================
-- Скрипт инициализации таблиц MariaDB для MyAPP.Drivers.MariaDB
-- ====================================================================

CREATE TABLE IF NOT EXISTS units_measure (
    unit_id     INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name        VARCHAR(50) NOT NULL,
    symbol      VARCHAR(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS counterparties (
    counterparty_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name            VARCHAR(100) NOT NULL,
    type            VARCHAR(50)  NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS materials (
    catalog_id      INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    catalog_number  VARCHAR(20)  NOT NULL,
    name            VARCHAR(100) NOT NULL,
    grade           VARCHAR(50)  NOT NULL,
    profile_size    VARCHAR(50)  NOT NULL,
    unit_id         INT UNSIGNED NOT NULL,
    stock_norm      DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,

    CONSTRAINT chk_material_stock_norm
        CHECK (stock_norm >= 0),

    CONSTRAINT fk_materials_unit
        FOREIGN KEY (unit_id)
        REFERENCES units_measure (unit_id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS documents (
    document_id      INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    document_number  VARCHAR(50) NOT NULL,
    record_date      DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    counterparty_id  INT UNSIGNED NOT NULL,
    document_type    INT NOT NULL,

    CONSTRAINT chk_document_counterparty_id
        CHECK (counterparty_id BETWEEN 1 AND 999999999),

    CONSTRAINT fk_documents_counterparty
        FOREIGN KEY (counterparty_id)
        REFERENCES counterparties (counterparty_id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS material_movements (
    record_id       INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    document_id     INT UNSIGNED NOT NULL,
    catalog_id      INT UNSIGNED NOT NULL,
    document_price  DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
    income          DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,
    expense         DECIMAL(14, 4) NOT NULL DEFAULT 0.0000,

    CONSTRAINT chk_mm_document_price
        CHECK (document_price >= 0),

    CONSTRAINT chk_mm_income
        CHECK (income >= 0),

    CONSTRAINT chk_mm_expense
        CHECK (expense >= 0),

    CONSTRAINT fk_mm_document
        FOREIGN KEY (document_id)
        REFERENCES documents (document_id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    CONSTRAINT fk_mm_catalog
        FOREIGN KEY (catalog_id)
        REFERENCES materials (catalog_id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

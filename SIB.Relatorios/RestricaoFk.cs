using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SIB.Data;

namespace SIB.Relatorios
{
    public class tabela
    {
        public string K_Table { get; set; }
        public string FK_Column { get; set; }
        public string PK_Table { get; set; }
        public string PK_Column { get; set; }
        public string Constraint_Name { get; set; }

    }

    public class listaItens
    {
        public int coluna_fk { get; set; }
    }

    public class RestricaoFk
    {
        public bool verificaRestricao(string tabela, Int32 valor)
        {
            string sqlQuery = "SELECT " +
                                "K_Table = FK.TABLE_NAME, " +
                                "FK_Column = CU.COLUMN_NAME, " +
                                "PK_Table = PK.TABLE_NAME, " +
                                "PK_Column = PT.COLUMN_NAME, " +
                                "Constraint_Name = C.CONSTRAINT_NAME " +
                                "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C " +
                                "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME " +
                                "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME " +
                                "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME " +
                                "INNER JOIN ( " +
                                "SELECT i1.TABLE_NAME, i2.COLUMN_NAME " +
                                "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 " +
                                "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME " +
                                "WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY' " +
                                ") PT ON PT.TABLE_NAME = PK.TABLE_NAME " +
                                "WHERE PK.TABLE_NAME = '" + tabela + "' " +                                                                
                                "ORDER BY " +
                                "1,2,3,4 ";

            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            List<tabela> listaTabelas = _dbContext.Database.SqlQuery<tabela>(sqlQuery).ToList();

            bool temReferencia = false;
            
            foreach (var item in listaTabelas.ToList())
            {
                string sql = "SELECT " + item.FK_Column + " as coluna_fk FROM " + item.K_Table + " WHERE " + item.FK_Column + " = " + valor;

                if (_dbContext.Database.SqlQuery<listaItens>(sql).ToList().Count > 0)
                {
                    temReferencia = true;
                    break;
                }
            }

            return temReferencia;
        }

        public bool verificaRestricaoEmpresaEndereco(string tabela, Int32 valor)
        {
            string sqlQuery = "SELECT " +
                                "K_Table = FK.TABLE_NAME, " +
                                "FK_Column = CU.COLUMN_NAME, " +
                                "PK_Table = PK.TABLE_NAME, " +
                                "PK_Column = PT.COLUMN_NAME, " +
                                "Constraint_Name = C.CONSTRAINT_NAME " +
                            "FROM " +
                                "INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C " +
                            "INNER JOIN " +
                                "INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK " +
                            "ON " +
                                "C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME " +
                            "INNER JOIN " +
                                "INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK " +
                            "ON " +
                                "C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME " +
                            "INNER JOIN " +
                                "INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU " +
                            "ON " +
                                "C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME " +
                            "INNER JOIN " +
                                "( SELECT " +
                                        "i1.TABLE_NAME, i2.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 " +
                                  "INNER JOIN " +
                                        "INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 " +
                                  "ON " +
                                        "i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME " +
                                  "WHERE " +
                                        "i1.CONSTRAINT_TYPE = 'PRIMARY KEY' ) " +
                                "PT " +
                            "ON " +
                                "PT.TABLE_NAME = PK.TABLE_NAME " +
                            "WHERE " +
                                "PK.TABLE_NAME = 'empresa' " +
                            "AND " +
                                "FK.TABLE_NAME != 'endereco' " +
                            "ORDER BY " +
                                "1,2,3,4 ";

            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            List<tabela> listaTabelas = _dbContext.Database.SqlQuery<tabela>(sqlQuery).ToList();

            bool temReferencia = false;

            foreach (var item in listaTabelas.ToList())
            {
                string sql = "SELECT " + item.FK_Column + " as coluna_fk FROM " + item.K_Table + " WHERE " + item.FK_Column + " = " + valor;

                if (_dbContext.Database.SqlQuery<listaItens>(sql).ToList().Count > 0)
                {
                    temReferencia = true;
                    break;
                }
            }

            return temReferencia;
        }
    }
}
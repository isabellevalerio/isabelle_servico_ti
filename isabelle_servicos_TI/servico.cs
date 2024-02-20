using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isabelle_servico_ti
{
    public partial class servico : Form
    {
        // Variaveis Publicas para o MySQL
        string servidor;
        MySqlConnection conexao;
        MySqlCommand comando;
        string idREGISTRO;

        public servico()
        {

            InitializeComponent();

            // Utilização das variaveis publicas para o MySQL
            servidor = "Server=localhost;Database=isabelle_servico_ti;Uid=root;Pwd=";
            conexao = new MySqlConnection(servidor);
            comando = conexao.CreateCommand();

            ATUALIZA_SERVICO();

        }
        private void ATUALIZA_SERVICO()
        {
            try
            {
                conexao.Open();

                comando.CommandText = "SELECT * FROM tbl_servico;";
                MySqlDataAdapter adaptadorAgenda = new MySqlDataAdapter(comando);
                DataTable tableAGENDA = new DataTable();
                adaptadorAgenda.Fill(tableAGENDA);


            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            label10.ForeColor = Color.Black;
            label12.ForeColor = Color.Black;
            label9.ForeColor = Color.Black;
            try
            {
                if (textboxNome.Text != "" && textboxEmail.Text != "" && textboxValorTotal.Text != "")
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO tbl_servico( data_entrada, status, observacoes, caracteristicas_equipamento, queixa_cliente, problemas, sugestao, valor_total, nome, sobrenome, email, celular ) VALUES ('" + datetimeDataEntrada.Text + "', '" + textboxStatus.Text + "', '" + textboxObservacoes.Text + "', '" + textboxCaracteristicas.Text + "', '" + textboxQueixaCliente.Text + "', '" + textboxProblemas.Text + "', '" + textboxSolucao.Text + "', '" + textboxValorTotal.Text + "', '" + textboxNome.Text + "', '" + textboxSobrenome + "', '" + textboxEmail + "', '" + textboxCelular + "' );";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Resgistrado com sucesso!");
                    
                }
                else
                {
                    MessageBox.Show("Nome, Email e/ou Valor Total do equipamento estão em BRANCO! Por favor preencha!");
                    if (textboxStatus.Text == "")
                    {
                        textboxNome.Focus();
                        label10.ForeColor = Color.Red;

                    }
                    if (textboxValorTotal.Text == "")
                    {
                        textboxValorTotal.Focus();
                        label9.ForeColor = Color.Red;
                    }
                    else
                    {
                        textboxEmail.Focus();
                        label12.ForeColor = Color.Red;
                    }

                }
            }
            catch (Exception erro_mysql)
            {
                // Mensagem de erro - MySQL
                // MessageBox.Show(erro_mysql.Message);

                // Mensagem de erro - USUÁRIO
                MessageBox.Show("Erro de Sistema. Solicite ajuda!");
            }
            finally
            {
                conexao.Close();

            }

         
        }

        private void buttonALTERAR_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "UPDATE tbl_servico SET ( data_entrada, status, observacoes, caracteristicas_equipamento, queixa_cliente, problemas, sugestao, valor_total, nome, sobrenome, emal, celular ) VALUES ('" + datetimeDataEntrada.Text + "', '" + textboxStatus.Text + "', '" + textboxObservacoes.Text + "', '" + textboxCaracteristicas.Text + "', '" + textboxQueixaCliente.Text + "', '" + textboxProblemas.Text + "', '" + textboxSolucao.Text + "', '" + textboxValorTotal.Text + "', '" + textboxNome + "', '" + textboxSobrenome + "', '" + textboxEmail + "', '" + textboxCelular + "' );";
                int resultado = comando.ExecuteNonQuery();
                if (resultado > 0)
                {
                    MessageBox.Show("Atualizado com sucesso! - " + resultado + " registros atualizados...");
                }
            }
            catch (Exception erro_mysql)
            {
                // Mensagem de erro - MySQL
                // MessageBox.Show(erro_mysql.Message);

                // Mensagem de erro - USUÁRIO
                MessageBox.Show("Erro de Sistema. Solicite ajuda!");
            }
            finally
            {
                conexao.Close();
            }
        }

        private void buttonAPAGAR_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este registro?", "Atenção!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    conexao.Open();

                    comando.CommandText = "DELETE FROM tbl_servico WHERE id = " + idREGISTRO + ";";
                    int resultado = comando.ExecuteNonQuery();
                    if (resultado > 0)
                    {
                        MessageBox.Show(" Removido(s) com sucesso! - " + resultado + " registros removidos...");
                    }
                    

                }
                catch (Exception erro_mysql)
                {
                    // Mensagem de erro - MySQL
                    // MessageBox.Show(erro_mysql.Message);

                    // Mensagem de erro - USUÁRIO
                    MessageBox.Show("Erro de Sistema. Solicite ajuda!");
                }
                finally
                {
                    conexao.Close();
                }
            }
            else
            {
                MessageBox.Show("NÃO");
            }
        }

        private void DataGridViewSERVICO_MouseDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idREGISTRO = DataGridViewSERVICO.CurrentRow.Cells[0].Value.ToString();

           datetimeDataEntrada.Text = DataGridViewSERVICO.CurrentRow.Cells[1].Value.ToString();
           textboxStatus.Text = DataGridViewSERVICO.CurrentRow.Cells[2].Value.ToString();
           textboxObservacoes.Text = DataGridViewSERVICO.CurrentRow.Cells[3].Value.ToString();
           textboxCaracteristicas.Text = DataGridViewSERVICO.CurrentRow.Cells[4].Value.ToString();
           textboxQueixaCliente.Text = DataGridViewSERVICO.CurrentRow.Cells[5].Value.ToString();
           textboxProblemas.Text = DataGridViewSERVICO.CurrentRow.Cells[6].Value.ToString();
           textboxSolucao.Text = DataGridViewSERVICO.CurrentRow.Cells[7].Value.ToString();
           textboxValorTotal.Text = DataGridViewSERVICO.CurrentRow.Cells[8].Value.ToString();
           textboxNome.Text = DataGridViewSERVICO.CurrentRow.Cells[9].Value.ToString();
           textboxSobrenome.Text = DataGridViewSERVICO.CurrentRow.Cells[10].Value.ToString();
           textboxEmail.Text = DataGridViewSERVICO.CurrentRow.Cells[11].Value.ToString();
           textboxCelular.Text = DataGridViewSERVICO.CurrentRow.Cells[12].Value.ToString();



        }
    }
    }
















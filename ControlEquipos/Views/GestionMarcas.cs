using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlEquipos.Models;

namespace ControlEquipos.Views
{
    public partial class GestionMarcas : Form
    {


        ControlEquipos.Models.marcas oMarca = null;
        private int? idMarca;
        public GestionMarcas(int? idMarca = null)
        {
            //Iniciarlizar el formulario (Dibujarlo)
            InitializeComponent();
            this.idMarca = idMarca;
            // Si ID marca no es nulo, es modo edición

            if (idMarca != null)
            {
                //cargar datos
                using (controlequiposEntities db = new controlequiposEntities())

                {
                    oMarca = db.marcas.Find(idMarca);
                    this.txtNombre.Text = oMarca.nom_marca;
                }
            }
        }

        private void GestionMarcas_Load(object sender, EventArgs e)
        {
            this.txtNombre.Select();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que los datos obligatorios se hayan  diligencíado
            if (string.IsNullOrEmpty(this.txtNombre.Text))
            {
                MessageBox.Show("Los campos guardados con(*) son obligatorios");
            }
            else
            {
                using (controlequiposEntities db = new controlequiposEntities())
                {
                    if (idMarca == null)
                    {
                        oMarca = new marcas();
                    }
                    oMarca.nom_marca = this.txtNombre.Text;

                    if (idMarca == null)
                    {
                        db.marcas.Add(oMarca);
                    }
                    else
                    {
                        db.Entry(oMarca).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                    this.Close();

                }


            }
        }
    }
}
    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfExample
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IMisServicios
    {

        [OperationContract]
        List<EmployeeDetails> GetEmployeeDetails(string EmployeeName);

        [OperationContract]
        string InsertEmployeeDetails(EmployeeDetails EmployeeInfo);

        [OperationContract]
        List<EmployeeDetails> GetAllEmployee();

        [OperationContract]
        bool UpdateEmployee(EmployeeDetails EmployeeID);

        [OperationContract]
        bool DeleteEmployee(int EmployeeInfo);

        [OperationContract]
        bool FindById(int id);
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class EmployeeDetails
    {
        int id;
        string name = string.Empty;
        int salary;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
    }
}

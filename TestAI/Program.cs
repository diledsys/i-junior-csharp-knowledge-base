using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //напиши метод,который будет давать управления объектом в Unity в лево в право вперед назад и пробел прыжок.

            Console.WriteLine(
                "void Update()\n" +
                "{\n" +
                "    float moveHorizontal = Input.GetAxis(\"Horizontal\");\n" +
                "    float moveVertical = Input.GetAxis(\"Vertical\");\n" +
                "\n" +
                "    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);\n" +
                "    transform.Translate(movement * speed * Time.deltaTime, Space.World);\n" +
                "\n" +
                "    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)\n" +
                "    {\n" +
                "        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);\n" +
                "        isGrounded = false;\n" +
                "    }\n" +
                "}\n"
                );
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;


public class EXPERIMENTAL_XDocumentCreation : MonoBehaviour
{
    void Start()
    {
        ToyingWithXmlLinq();
    }

    private void ToyingWithXmlLinq()
    {

        //-----------Basic XDocument Functionality--------------------------------------------

        //Create new XDocument with Xml Declaration
        XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

        //Add a Root Element
        xmlDocument.Add(new XElement("ROOT"));

        //Get Root Element and Add Element with a value
        xmlDocument.Root.Add(new XElement("FIRST_ELEMENT", 25));

        //Create an Element
        XElement second = new XElement("SECOND_ELEMENT");
        
        //Add 3 Elements, with values, to the previous Element created
        second.Add(new XElement("POINTS", 3));
        second.Add(new XElement("COINS", 1));
        second.Add(new XElement("POTATOES", 500));

        //Get Root Element and Add Element in the Last Place
        xmlDocument.Root.Add(second);

        //Create an Element
        XElement third = new XElement("THIRD_ELEMENT");

        //Add Attribute to Element previously created
        third.SetAttributeValue("Attribute", "value");

        //Add 1 Elements, with values, to the previous Element created
        third.Add(new XElement("Missiles", 6));

        //Create an Element
        XElement fourth = new XElement("FIRST_ELEMENT", 2);

        //Get Root Element and Add Element in the Last Place
        xmlDocument.Root.Add(fourth);

        //Get Root Element and Add Element in the First Place
        xmlDocument.Root.AddFirst(third);

        //------------------------------------------------------------------------------------


        //--------Gets and Adds---------------------------------------------------------------

        //Get Root Element
        XElement rootElement = xmlDocument.Root;

        //Get FIRST_ELEMENT and Edit it's Value
        XElement firstElement = rootElement.Element("FIRST_ELEMENT"); 
        firstElement.Value = "1";

        //Get THIRD_ELEMENT and Edit Missiles Element Value
        XElement thirdElement = rootElement.Element("THIRD_ELEMENT");
        thirdElement.Element("Missiles").Value = "8";

        //Get Count of Children in Root Element
        int countRootElements = rootElement.Elements().Count();

        //Get Count of Element FIRST_ELEMENT
        int countFirstElement = rootElement.Elements("FIRST_ELEMENT").Count();

        Debug.Log(countRootElements + " | " + countFirstElement);

        //------------------------------------------------------------------------------------

        //Saving XDocument
        xmlDocument.Save("Assets/Resources/XmlCreationDocument.xml");
    }
}

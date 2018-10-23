package BL;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.DocumentBuilder;
import org.w3c.dom.Document;
import java.io.File;
public class Configuration {

    public static void pullConfig(String configName) {



        try {

            File fXmlFile = new File("res/values/BaseConfig.xml");
            DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
            Document doc = dBuilder.parse(fXmlFile);
            doc.getDocumentElement().normalize();
            String s =  doc.getElementsByTagName(configName).item(0).toString();


        } catch (Exception e) {
            e.printStackTrace();
        }
    }


}

package BL.missions;

import org.apache.commons.net.ftp.FTP;
import org.apache.commons.net.ftp.FTPClient;
import org.junit.Test;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

import SharedClasses.Config;


public class TakePictureMissionTest {
    @Test
    public void testScript(){

    }
    @Test
    public void uploadFile() {
        FTPClient client = new FTPClient();
        FileInputStream fis = null;

        try {
            client.connect(Config.DST_ADDRESS, 14147);
            client.login("bar", "");
            client.setFileType(FTP.BINARY_FILE_TYPE);
            String filename = "myPDF.pdf";
            fis = new FileInputStream(filename);

            client.storeFile("temp.pdf", fis);
            fis.close();
            client.logout();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}
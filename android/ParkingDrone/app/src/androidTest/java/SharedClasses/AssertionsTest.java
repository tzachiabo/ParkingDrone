package SharedClasses;

import org.junit.Test;

import static org.junit.Assert.*;

public class AssertionsTest {
    @Test
    public void tests(){
        Assertions.verify(false,"lalalala");
    }

}
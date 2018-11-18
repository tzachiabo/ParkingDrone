package SharedClasses;

import org.junit.runner.RunWith;
import org.junit.runners.Suite;
import BL.BLTestSuite;

@RunWith(Suite.class)
@Suite.SuiteClasses({ BLTestSuite.class, SharedClassesTestSuite.class} )
public class AllUnitTests {

}




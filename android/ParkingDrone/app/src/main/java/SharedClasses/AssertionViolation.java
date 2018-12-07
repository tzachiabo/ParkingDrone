package SharedClasses;

public class AssertionViolation extends RuntimeException {
    String m_msg;

    public AssertionViolation(String msg){
        m_msg = msg;
    }
}

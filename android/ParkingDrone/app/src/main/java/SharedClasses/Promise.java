package SharedClasses;

public abstract class Promise {
    boolean hasExecuted=false;

    public void success(){
        if (!hasExecuted){
            hasExecuted=true;
            onSuccess();
        }
        else{
            Logger.error("promise.failed was trigered when promise allready executed");
        }
    }

    public void failed(){
        if (!hasExecuted){
            hasExecuted=true;
            onFailed();
        }
        else{
            Logger.error("promise.failed was trigered when promise allready executed");
        }

    }
    protected abstract void onSuccess();
    public abstract void onFailed();


}

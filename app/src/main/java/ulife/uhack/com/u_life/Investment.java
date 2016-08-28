package ulife.uhack.com.u_life;

import android.app.Activity;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class Investment extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_investment);
    }


    public void pay(View v)
    {
        Intent i = new Intent(this, payment.class);
        this.startActivity(i);
    }
}

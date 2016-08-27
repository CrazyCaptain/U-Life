package ulife.uhack.com.u_life;

import android.app.Activity;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class MainMenu extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main_menu);
    }

    public void viewLife(View v)
    {
        launchActivity(LIFE);
    }
    public void viewEducation(View v)
    {
        launchActivity(EDUCATION);
    }
    public void viewAuto(View v)
    {
        launchActivity(AUTO);
    }
    public void viewInvestment(View v)
    {
        launchActivity(INVESTMENT);
    }

    public static final int LIFE = 1, EDUCATION = 2, AUTO = 3, INVESTMENT = 4;

    private void launchActivity(int activityCode)
    {
        //class toCall;
        Intent i = null;
        if(activityCode == LIFE)
        {
            i = new Intent(this, Life.class);
        }
        else if (activityCode == EDUCATION)
        {
            i = new Intent(this, Education.class);
        }
        else if (activityCode == AUTO)
        {
            i = new Intent(this, Auto.class);
        }
        else if (activityCode == INVESTMENT)
        {
            i = new Intent(this, Investment.class);
        }
        this.startActivity(i);
    }

}

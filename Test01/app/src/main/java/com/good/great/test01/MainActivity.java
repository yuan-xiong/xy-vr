package com.good.great.test01;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.webkit.WebView;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    public void javaTestFunc(String strFromUnity) {

        //java to unity
        UnityPlayer.UnitySendMessage("AndroidManager", "SetJavaLog", strFromUnity + "HelloWorld");

        startIntent();

        Toast.makeText(MainActivity.this, "javaTestFunc 2", Toast.LENGTH_LONG).show();
    }

    private void startIntent() {
        Intent intent = new Intent(MainActivity.this, WebViewActivity.class);
        startActivity(intent);
    }

    private void showWebView() {
        Toast.makeText(MainActivity.this, "showWebView", Toast.LENGTH_LONG).show();
        WebView webView = new WebView(MainActivity.this);
        String mUrl = "http://www.baidu.com";
        webView.loadUrl(mUrl);
        webView.getSettings().setJavaScriptEnabled(true);
        webView.setWebViewClient(new WebViewClient());
    }

    private class WebViewClient extends android.webkit.WebViewClient {
        @Override
        public boolean shouldOverrideUrlLoading(WebView view, String url) {
            //这里实现的目标是在网页中继续点开一个新链接，还是停留在当前程序中
            view.loadUrl(url);
            return super.shouldOverrideUrlLoading(view, url);

        }
    }
}



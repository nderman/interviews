<?php

namespace Realmdigital\Web;

/**
 *
 */
class Product
{
    private $curl;
    private $eanlist = 'http://192.168.0.241/eanlist?type=Web';

    public function __construct()
    {
        $this->curl   = curl_init();
        $this->result = [];

    }

    public function get_by_id($id)
    {
        return $this->request(["id" => $id]);

    }

    public function get_by_name($name)
    {
        return $this->request(["name" => $name]);
    }

    public function request($requestData)
    {

        curl_setopt($curl, CURLOPT_URL, $eanlist);
        curl_setopt($curl, CURLOPT_POST, 1);
        curl_setopt($curl, CURLOPT_POSTFIELDS, $requestData);
        curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
        $response = curl_exec($curl);
        $response = json_decode($response);
        curl_close($curl);
        $result = [];
        for ($i = 0; $i < count($response); $i++) {
            $prod           = array();
            $prod['ean']    = $response[$i]['barcode'];
            $prod["name"]   = $response[$i]['itemName'];
            $prod["prices"] = array();
            for ($j = 0; $j < count($response[$i]['prices']); $j++) {
                if ($response[$i]['prices'][$j]['currencyCode'] != 'ZAR') {
                    $p_price            = array();
                    $p_price['price']   = $response[$i]['prices'][$j]['sellingPrice'];
                    $p_price['curreny'] = $response[$i]['prices'][$j]['currencyCode'];
                    $prod["prices"][]   = $p_price;
                }
            }
            $result[] = $prod;
        }
        return $result;
    }

}

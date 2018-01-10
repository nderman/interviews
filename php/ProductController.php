<?php

namespace Realmdigital\Web\Controller;

use DDesrosiers\SilexAnnotations\Annotations as SLX;
use Realmdigital\Web\Product;
use Silex\Application;

/**
 * @SLX\Controller(prefix="product/")
 */
class ProductController
{

    /**
     * @SLX\Route(
     *      @SLX\Request(method="GET", uri="/{id}")
     * )
     * @param Application $app
     * @param $name
     * @return
     */
    public function getById_GET(Application $app, $id)
    {
        $product = new Product;
        return $app->render('products/product.detail.twig', $product->get_by_id($id));
    }

    /**
     * @SLX\Route(
     *      @SLX\Request(method="GET", uri="/search/{name}")
     * )
     * @param Application $app
     * @param $name
     * @return
     */
    public function getByName_GET(Application $app, $name)
    {
        $product = new Product;
        return $app->render('products/product.detail.twig', $product->get_by_name($name));
    }

}

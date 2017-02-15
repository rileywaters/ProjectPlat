
import javafx.application.*;
import javafx.stage.*;
import javafx.scene.*;
import javafx.geometry.*;
import javafx.scene.canvas.*;
import javafx.scene.paint.Color;
import javafx.scene.text.*;
import javafx.scene.layout.*;
import javafx.scene.control.*;

public class Main extends Application {

    Stage window;
    Scene scene1, scene2, scene3;

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage primaryStage) {
        window = primaryStage;

        //Scene 1
        //Main menu
        Label mainMenu = new Label("Main Menu - Add graphics here?");
        BorderPane layout = new BorderPane();
        layout.setBottom(addMenu());
        layout.setTop(mainMenu);
        scene1 = new Scene(layout, 600, 300);

        //Scene 2
        //This is just a graphics test of stuff we can put in here
        Group root = new Group();
        Canvas canvas = new Canvas( 600, 300 );
        root.getChildren().add( canvas );
        GraphicsContext gc = canvas.getGraphicsContext2D();
        gc.setFill( Color.RED );
        gc.setStroke( Color.BLACK );
        gc.setLineWidth(2);
        Font theFont = Font.font( "Times New Roman", FontWeight.BOLD, 48 );
        gc.setFont( theFont );
        gc.fillText( "Level 1", 60, 50 );
        gc.strokeText( "Level 1", 60, 50 );
        scene2 = new Scene(root, 600, 300);

        //Scene 3
        //Options
        scene3 = new Scene(addOptions(), 600, 300);


        //Display scene 1 at first
        window.setScene(scene1);
        window.setTitle("Project Plat");
        window.show();
    }

    public HBox addMenu() {
        HBox hbox = new HBox();
        hbox.setPadding(new Insets(15, 12, 15, 12));
        hbox.setSpacing(10);


        Button newGameButton = new Button("New Game");
        newGameButton.setPrefSize(100, 20);

        Button optionsButton = new Button("Options");
        optionsButton.setPrefSize(100, 20);

        Button closeButton = new Button("Close");
        closeButton.setPrefSize(100, 20);
        hbox.getChildren().addAll(newGameButton, optionsButton, closeButton);

        newGameButton.setOnAction(e -> window.setScene(scene2));
        optionsButton.setOnAction(e -> window.setScene(scene3));
        closeButton.setOnAction(e -> Platform.exit());
        return hbox;
    }

    public HBox addOptions() {
        HBox hbox = new HBox();

        hbox.setPadding(new Insets(15, 12, 15, 12));
        hbox.setSpacing(10);


        final ComboBox graphics = new ComboBox();
        graphics.getItems().addAll(
                "Low",
                "Medium",
                "High"
        );
        graphics.setValue("Medium");

        final Slider volume = new Slider(0, 100, 50);
        final Label volumeLabel = new Label("Volume:");

        Button backButton = new Button("Save and Back");
        backButton.setPrefSize(100, 20);
        hbox.getChildren().addAll(backButton, graphics, volumeLabel, volume);

        backButton.setOnAction(e -> window.setScene(scene1));
        return hbox;
    }
}
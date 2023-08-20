using UnityEngine;

public class DraggablePuzzlePiece : MonoBehaviour
{
    [SerializeField] string FriendName;
    public Transform correctPosition; 
    private bool isDragging = false;
    private Vector3 offset;
    [SerializeField] PuzzleCompleteCheck check;
    [SerializeField] GameObject Minigame1;
    [SerializeField] GameObject Minigame2;
    private void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        if (GetComponent<BoxCollider2D>().OverlapPoint(mousePosition))
        {
            isDragging = true;
            offset = transform.position - mousePosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        float distanceToCorrectPosition = Vector3.Distance(transform.position, correctPosition.position);
        if (distanceToCorrectPosition < 0.5f) 
        {
            SnapToCorrectPosition(); 
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            transform.position = mousePosition + offset;
        }
    }

    private void SnapToCorrectPosition()
    {
        check.PuzzleCompleteNum++;
        transform.position = correctPosition.position;
        isDragging = false;
        GetComponent<Collider2D>().enabled = false;
        if(FriendName == "Bean")
        {
            Minigame1.SetActive(true);
            Minigame2.SetActive(false);
        }
    }
}
